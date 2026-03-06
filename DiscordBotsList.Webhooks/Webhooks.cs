using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DiscordBotsList.Webhooks
{
    public interface IWebhookListener
    {
        /// <summary>
        ///     A user has connected to your webhook integration.
        /// </summary>
        Task OnIntegrationCreate(HttpContext context, IntegrationCreatePayload payload, string trace) => DefaultResponse(context);

        /// <summary>
        ///     A user has disconnected from your webhook integration.
        /// </summary>
        Task OnIntegrationDelete(HttpContext context, IntegrationDeletePayload payload, string trace) => DefaultResponse(context);

        /// <summary>
        ///     Test webhook sent from the dashboard.
        /// </summary>
        Task OnTest(HttpContext context, TestPayload test, string trace) => DefaultResponse(context);

        /// <summary>
        ///     Fired when a user votes for your project.
        /// </summary>
        Task OnVoteCreate(HttpContext context, VoteCreatePayload vote, string trace) => DefaultResponse(context);

        private static Task DefaultResponse(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 204;
            }

            return Task.CompletedTask;
        }
    }

    internal class Payload {
        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("data")]
        public JsonElement Data { get; init; }
    }

    public abstract class Webhooks
    {
        private byte[] Authorization;
        private readonly JsonSerializerOptions SerializerOptions = new()
        {
            Converters = {new ULongToStringConverter(), new PlatformConverter(), new ProjectTypeConverter()}
        };

        public Webhooks(string authorization) => SetAuthorization(authorization);

        public void SetAuthorization(string newAuthorization) => Authorization = Encoding.UTF8.GetBytes(newAuthorization);

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

        public RequestDelegate Listener(IWebhookListener listener)
        {
            return async (context) =>
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

                    var hash = Convert.ToHexString(HMACSHA256.HashData(Authorization, transformBuffer)).ToLowerInvariant();

                    if (!parsedSignature["v1"].Equals(hash) && !context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 401;

                        await context.Response.WriteAsync("Invalid Authorization");

                        return;
                    }

                    var payload = JsonSerializer.Deserialize<Payload>(body, SerializerOptions);

                    if (payload != null)
                    {
                        switch (payload.Type)
                        {
                            case "integration.create": await Dispatch<IntegrationCreatePayload>(listener.OnIntegrationCreate, context, payload, trace); break;
                            case "integration.delete": await Dispatch<IntegrationDeletePayload>(listener.OnIntegrationDelete, context, payload, trace); break;
                            case "webhook.test": await Dispatch<TestPayload>(listener.OnTest, context, payload, trace); break;
                            case "vote.create": await Dispatch<VoteCreatePayload>(listener.OnVoteCreate, context, payload, trace); break;
                        }

                        return;
                    }
                }
                catch
                {}

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsync("Bad Request");
                }
            };
        }
    }
}