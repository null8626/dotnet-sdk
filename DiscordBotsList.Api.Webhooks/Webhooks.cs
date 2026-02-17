using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DiscordBotsList.Api.Webhooks
{
    public class Webhooks
    {
        private readonly byte[] authorization;
        private readonly JsonSerializerOptions serializerOptions;

        public Webhooks(string authorization)
        {
            this.authorization = Encoding.UTF8.GetBytes(authorization);

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new ULongToStringConverter());
            serializerOptions.Converters.Add(new PlatformConverter());
            serializerOptions.Converters.Add(new ProjectTypeConverter());
        }
        
        public delegate Task VoteCreateDelegate(HttpContext context, VoteCreatePayload vote, StringValues trace);
        public delegate Task TestDelegate(HttpContext context, TestPayload test, StringValues trace);

        public async Task<(T payload, StringValues trace)> ParsePayload<T>(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-topgg-signature", out var signatureHeader) && !context.Response.HasStarted)
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Missing Top.gg Signature");

                return default;
            }

            context.Request.Headers.TryGetValue("x-topgg-trace", out var trace);

            try
            {
                var parsedSignature = signatureHeader.First().Split(',').Select(part => part.Split('=')).ToDictionary(part => part[0], part => part[1]);

                using var bodyStream = new MemoryStream();

                await context.Request.Body.CopyToAsync(bodyStream);
                var body = bodyStream.ToArray();
                var transformBuffer = Encoding.UTF8.GetBytes($"{parsedSignature["t"]}.").Concat(body).ToArray();

                var hash = Convert.ToHexString(HMACSHA256.HashData(authorization, transformBuffer)).ToLowerInvariant();

                if (!parsedSignature["v1"].Equals(hash) && !context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;

                    await context.Response.WriteAsync("Invalid Authorization");

                    return default;
                }

                return (JsonSerializer.Deserialize<T>(body, serializerOptions), trace);
            }
            catch {}

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync("Invalid Request");
            }

            return default;
        }

        public RequestDelegate VoteCreateListener(VoteCreateDelegate voteCreateDelegate)
        {
            return async (context) =>
            {
                var (vote, trace) = await ParsePayload<VoteCreatePayload>(context);

                if (vote != null)
                {
                    await voteCreateDelegate(context, vote, trace);

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 204;
                    }

                    return;
                }
            };
        }

        public RequestDelegate TestListener(TestDelegate testDelegate)
        {
            return async (context) =>
            {
                var (test, trace) = await ParsePayload<TestPayload>(context);

                if (test != null)
                {
                    await testDelegate(context, test, trace);

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 204;
                    }

                    return;
                }
            };
        }
    }
}