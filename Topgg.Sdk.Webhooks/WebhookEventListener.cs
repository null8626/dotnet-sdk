using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Topgg.Sdk.Webhooks.Payloads;
using Topgg.Sdk.Webhooks.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Text.Json.Serialization;

namespace Topgg.Sdk.Webhooks;

/// <summary>A Top.gg webhook event listener.</summary>
public abstract class WebhookEventListener
{
    private byte[] Secret;
    private readonly JsonSerializerOptions SerializerOptions = new()
    {
        Converters = { new ULongToStringConverter(), new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public WebhookEventListener(string secret) => SetSecret(secret);

    /// <summary>Sets the webhook secret to use to authorize external requests.</summary>
    /// <param name="newSecret">The new webhook secret to use to authorize external requests.</param>
    public void SetSecret(string newSecret) => Secret = Encoding.UTF8.GetBytes(newSecret);

    private async Task Dispatch<T>(Func<HttpContext, T, string, Task> callback, HttpContext context, Payload payload, StringValues trace)
    {
        var data = payload.Data.Deserialize<T>(SerializerOptions);

        if (data == null)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync("Bad Request");
            }
        }
        else
        {
            try
            {
                await callback(context, data, trace.First());
            }
            catch
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsync("Internal Server Error");
                }
            }
        }
    }

    /// <summary>The handler method to be passed to ASP.NET Core.</summary>
    /// <param name="context">The HTTP request context from ASP.NET Core.</param>
    public async void Handler(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("x-topgg-signature", out var signatureHeader) && !context.Response.HasStarted)
        {
            context.Response.StatusCode = 401;

            await context.Response.WriteAsync("Missing Top.gg Signature");

            return;
        }

        context.Request.Headers.TryGetValue("x-topgg-trace", out var trace);

        try
        {
            var parsedSignature = signatureHeader.First().Split(',').Select(part => part.Split('=')).ToDictionary(part => part[0], part => part[1]);

            using var bodyStream = new MemoryStream();

            await context.Request.Body.CopyToAsync(bodyStream);
            var body = bodyStream.ToArray();
            var transformBuffer = Encoding.UTF8.GetBytes($"{parsedSignature["t"]}.").Concat(body).ToArray();

            var hash = Convert.ToHexString(HMACSHA256.HashData(Secret, transformBuffer)).ToLowerInvariant();

            if (!parsedSignature["v1"].Equals(hash) && !context.Response.HasStarted)
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Invalid Secret");

                return;
            }

            var payload = JsonSerializer.Deserialize<Payload>(body, SerializerOptions);

            if (payload != null)
            {
                switch (payload.Type)
                {
                    case "integration.create": await Dispatch<IntegrationCreatePayload>(OnIntegrationCreate, context, payload, trace); break;
                    case "integration.delete": await Dispatch<IntegrationDeletePayload>(OnIntegrationDelete, context, payload, trace); break;
                    case "webhook.test": await Dispatch<TestPayload>(OnTest, context, payload, trace); break;
                    case "vote.create": await Dispatch<VoteCreatePayload>(OnVoteCreate, context, payload, trace); break;
                }

                return;
            }
        }
        catch
        { }

        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 400;

            await context.Response.WriteAsync("Bad Request");
        }
    }

    /// <summary>Registers a listener that fires when a user has connected to your webhook integration.</summary>
    /// <param name="context">The HTTP request context from ASP.NET Core.</param>
    /// <param name="payload">The webhook payload.</param>
    /// <param name="trace">The payload's x-topgg-trace header for debugging and correlating requests with Top.gg support.</param>
    /// <returns>The response for this request.</returns>
    public virtual Task OnIntegrationCreate(HttpContext context, IntegrationCreatePayload payload, string trace) => DefaultResponse(context);

    /// <summary>Registers a listener that fires when a user has disconnected from your webhook integration.</summary>
    /// <param name="context">The HTTP request context from ASP.NET Core.</param>
    /// <param name="payload">The webhook payload.</param>
    /// <param name="trace">The payload's x-topgg-trace header for debugging and correlating requests with Top.gg support.</param>
    /// <returns>The response for this request.</returns>
    public virtual Task OnIntegrationDelete(HttpContext context, IntegrationDeletePayload payload, string trace) => DefaultResponse(context);

    /// <summary>Registers a listener that fires when a test webhook was sent from the dashboard.</summary>
    /// <param name="context">The HTTP request context from ASP.NET Core.</param>
    /// <param name="payload">The webhook payload.</param>
    /// <param name="trace">The payload's x-topgg-trace header for debugging and correlating requests with Top.gg support.</param>
    /// <returns>The response for this request.</returns>
    public virtual Task OnTest(HttpContext context, TestPayload payload, string trace) => DefaultResponse(context);

    /// <summary>Registers a listener that fires when a user votes for your project.</summary>
    /// <param name="context">The HTTP request context from ASP.NET Core.</param>
    /// <param name="payload">The webhook payload.</param>
    /// <param name="trace">The payload's x-topgg-trace header for debugging and correlating requests with Top.gg support.</param>
    /// <returns>The response for this request.</returns>
    public virtual Task OnVoteCreate(HttpContext context, VoteCreatePayload payload, string trace) => DefaultResponse(context);

    private static Task DefaultResponse(HttpContext context)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 204;
        }

        return Task.CompletedTask;
    }
}