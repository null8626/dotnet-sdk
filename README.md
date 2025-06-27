# Top.gg .NET SDK

The community-maintained .NET library for Top.gg.

## Installation

### Main API wrapper

#### Library agnostic

```powershell
> Install-Package DiscordBotsList.Api
```

#### Discord.NET-based

```powershell
> Install-Package DiscordBotsList.Api.Adapter.Discord.Net
```

### Webhooks only

```powershell
> Install-Package DiscordBotsList.Api.Webhooks
```

## Setting up

### Library agnostic

```cs
var client = new DiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

### Discord.NET-based

```cs
var discordNetClient = ...;
var client = new DiscordNetDblApi(discordNetClient, "TOPGG_TOKEN");
```

## Usage

### Getting a bot

```cs
//                                 Discord ID
var bot = await client.GetBotAsync(264811613708746752U);
```

### Getting several bots

#### With defaults

```cs
var bots = await client.GetBotsAsync();
var firstBot = bots.Items.First();
```

#### With explicit arguments

```cs
//                                   Sort by                   Limit  Offset
var bots = await client.GetBotsAsync(SortBotsBy.MonthlyPoints, 100,   1);
var firstBot = bots.Items.First();
```

### Getting your bot's voters

#### First page

```cs
var voters = await client.GetVotersAsync();
```

#### Subsequent pages

```cs
var voters = await client.GetVotersAsync(2);
```

### Check if a user has voted for your bot

```cs
//                                Discord ID
var voted = await client.HasVoted(661200758510977084U);
```

### Getting your bot's server count

```cs
var serverCount = await client.GetServerCountAsync();
```

### Posting your bot's server count

```cs
//                                  Server count
await client.UpdateServerCountAsync(bot.GetServerCount());
```

### Automatically posting your bot's server count every few minutes

With Discord.NET:

```cs
var submissionAdapter = client.CreateListener();

submissionAdapter.Start();

// ...

submissionAdapter.Stop(); // Optional
```

### Checking if the weekend vote multiplier is active

```cs
var isWeekend = await client.IsWeekendAsync();
```

### Generating widget URLs

#### Large

```cs
//                           Widget type             Discord ID
var widgetUrl = Widget.Large(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Votes

```cs
//                           Widget type             Discord ID
var widgetUrl = Widget.Votes(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Owner

```cs
//                           Widget type             Discord ID
var widgetUrl = Widget.Owner(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Social

```cs
//                            Widget type             Discord ID
var widgetUrl = Widget.Social(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

### Webhooks

#### Being notified whenever someone voted for your bot

With ASP.NET Core or Blazor:

```cs
using DiscordBotsList.Api.Webhooks;

namespace MyServer
{
    internal class MyVoteListener : IReceiver<Vote>
    {
        public Task Callback(Vote vote)
        {
            Console.WriteLine($"A user with the ID of {vote.VoterId} has voted us on Top.gg!");

            return Task.CompletedTask;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseMiddleware<VoteMiddleware<MyVoteListener>>("/votes", Environment.GetEnvironmentVariable("MY_TOPGG_WEBHOOK_SECRET"), new MyVoteListener());

            app.Map("/", async context =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });

            app.Run("http://localhost:8080");
        }
    }
}
```