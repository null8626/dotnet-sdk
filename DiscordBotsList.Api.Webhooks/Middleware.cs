using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Webhooks
{
    public class Middleware<Receiver, Event> where Receiver : IReceiver<Event>
    {
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly RequestDelegate _next;
        private readonly string _path;
        private readonly string _auth;
        private readonly Receiver _receiver;

        public Middleware(RequestDelegate next, string path, string auth, Receiver receiver)
        {
            _next = next;
            _path = path;
            _auth = auth;
            _receiver = receiver;

            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new ULongToStringConverter());
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(_path) && context.Request.Method == "POST")
            {
                if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationInput) || !authorizationInput.Equals(_auth))
                {
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Unauthorized");
                    }

                    return;
                }

                var data = await JsonSerializer.DeserializeAsync<Event>(context.Request.Body, _serializerOptions);

                if (data != null)
                {
                    await _receiver.Callback(data);

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
            }
            else
            {
                await _next(context);
            }
        }
    }

    public class VoteMiddleware<Receiver>(RequestDelegate next, string path, string auth, Receiver receiver) : Middleware<Receiver, Vote>(next, path, auth, receiver) where Receiver : IReceiver<Vote>
    {
    }
}