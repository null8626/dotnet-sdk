using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Tests
{
    public class Mocks : HttpMessageHandler
    {
        private static Stream StreamJson(string name) => typeof(Mocks).Assembly.GetManifestResourceStream($"DiscordBotsList.Api.Tests.mocks.{name}.json");

        internal static string ReadJson(string name)
        {
            using var stream = StreamJson(name);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.Host == "top.gg" && request.RequestUri.AbsolutePath.StartsWith("/api/v1/"))
            {
                var route = request.RequestUri.AbsolutePath[7..];

                if (request.Method == HttpMethod.Get)
                {
                    Stream body;

                    if (route == "/projects/@me")
                    {
                        body = StreamJson("GetSelf");
                    }
                    else if (route == "/projects/@me/votes/")
                    {
                        body = StreamJson("GetVote");
                    }
                    else if (route == "/projects/@me/votes")
                    {
                        body = StreamJson("GetVotes");
                    }
                    else
                    {
                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
                    }

                    var content = new StreamContent(body);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = content,
                    });
                }
                else if (request.Method == HttpMethod.Post && route == "/projects/@me/commands")
                {
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
                }
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}