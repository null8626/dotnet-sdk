using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DiscordBotsList.Api.Webhooks
{
    public class Webhooks
    {
        private readonly string authorization;
        private readonly JsonSerializerOptions serializerOptions;

        public Webhooks(string authorization)
        {
            this.authorization = authorization;

            serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new ULongToStringConverter());
        }
        
        public delegate Task VoteDelegate(HttpContext context, Vote vote);

        public RequestDelegate Listener(VoteDelegate voteDelegate)
        {
            return async (context) =>
            {
                if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationInput) || !authorizationInput.Equals(authorization))
                {
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 401;

                        await context.Response.WriteAsync("Unauthorized");

                        return;
                    }
                }

                var vote = await JsonSerializer.DeserializeAsync<Vote>(context.Request.Body, serializerOptions);

                if (vote != null)
                {
                    await voteDelegate(context, vote);

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 204;
                    }
                }
                else if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsync("Bad request");
                }
            };
        }
    }
}