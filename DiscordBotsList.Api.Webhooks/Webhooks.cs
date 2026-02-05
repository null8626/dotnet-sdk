using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        }
        
        public delegate Task VoteDelegate(HttpContext context, Vote vote);

        public RequestDelegate Listener(VoteDelegate voteDelegate)
        {
            return async (context) =>
            {
                if (!context.Request.Headers.TryGetValue("x-topgg-signature", out var signatureHeader) && !context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;

                    await context.Response.WriteAsync("Missing Top.gg Signature");

                    return;
                }

                try
                {
                    var parsedSignature = signatureHeader.First().Split(',').Select(part => part.Split('=')).ToDictionary(part => part[0], part => part[1]);

                    using var bodyStream = new MemoryStream();

                    await context.Request.Body.CopyToAsync(bodyStream);
                    var body = bodyStream.ToArray();
                    var transformBuffer = Encoding.UTF8.GetBytes($"{parsedSignature["t"]}.").Concat(body).ToArray();

                    var hmac = new HMACSHA256(authorization);

                    hmac.TransformFinalBlock(transformBuffer, 0, transformBuffer.Length);

                    if (!parsedSignature["v1"].Equals(Convert.ToHexString(hmac.Hash).ToLowerInvariant()) && !context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 401;

                        await context.Response.WriteAsync("Invalid Authorization");

                        return;
                    }

                    var vote = JsonSerializer.Deserialize<Vote>(body, serializerOptions);

                    if (vote != null)
                    {
                        await voteDelegate(context, vote);

                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = 204;
                        }

                        return;
                    }
                }
                catch
                {}

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsync("Invalid Request");
                }
            };
        }
    }
}