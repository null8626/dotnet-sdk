using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DiscordBotsList.Webhooks.Tests
{
    internal class CustomWebhooks() : Webhooks(Environment.GetEnvironmentVariable("TOPGG_WEBHOOK_SECRET"))
    {
        public override Task OnIntegrationCreate(HttpContext context, IntegrationCreatePayload payload, string trace) => DefaultResponse("IntegrationCreate", context, trace);
        public override Task OnIntegrationDelete(HttpContext context, IntegrationDeletePayload payload, string trace) => DefaultResponse("IntegrationDelete", context, trace);
        public override Task OnTest(HttpContext context, TestPayload payload, string trace) => DefaultResponse("Test", context, trace);
        public override Task OnVoteCreate(HttpContext context, VoteCreatePayload payload, string trace) => DefaultResponse("VoteCreate", context, trace);

        private static async Task DefaultResponse(string name, HttpContext context, string trace)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 200;

                await context.Response.WriteAsync($"{name},{trace}");
            }
        }
    }
}