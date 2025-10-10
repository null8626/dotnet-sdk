# Top.gg .NET SDK

The community-maintained .NET library for Top.gg.

## Chapters

- [Installation](#installation)
- [Setting up](#setting-up)
- [Usage](#usage)
  - [API v1](#api-v1)
    - [Getting your project's vote information of a user](#getting-your-projects-vote-information-of-a-user)
    - [Posting your bot's application commands list](#posting-your-bots-application-commands-list)
  - [API v0](#api-v0)
    - [Getting a bot](#getting-a-bot)
    - [Getting several bots](#getting-several-bots)
    - [Getting your project's voters](#getting-your-projects-voters)
    - [Check if a user has voted for your project](#check-if-a-user-has-voted-for-your-project)
    - [Getting your bot's statistics](#getting-your-bots-statistics)
    - [Posting your bot's statistics](#posting-your-bots-statistics)
    - [Automatically posting your bot's statistics every few minutes](#automatically-posting-your-bots-statistics-every-few-minutes)
    - [Checking if the weekend vote multiplier is active](#checking-if-the-weekend-vote-multiplier-is-active)
    - [Generating widget URLs](#generating-widget-urls)
  - [Webhooks](#webhooks)
    - [Being notified whenever someone voted for your project](#being-notified-whenever-someone-voted-for-your-project)

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

#### API v1

> **NOTE**: API v1 also includes API v0.

```cs
var client = new AuthV1DiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

#### API v0

```cs
var client = new AuthDiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

### Discord.NET-based

#### API v1

> **NOTE**: API v1 also includes API v0.

```cs
var discordNetClient = ...;
var client = new DiscordNetV1DblApi(discordNetClient, "TOPGG_TOKEN");
```

#### API v0

```cs
var discordNetClient = ...;
var client = new DiscordNetDblApi(discordNetClient, "TOPGG_TOKEN");
```

## Usage

### API v1

#### Getting your project's vote information of a user

```cs
var vote = await client.GetVoteAsync(661200758510977084);
```

#### Posting your bot's application commands list

```cs
// Array of application commands in Discord API's raw JSON format.
await client.UpdateBotCommandsAsync("[{\"options\":[],\"name\":\"test\",\"name_localizations\":null,\"description\":\"command description\",\"description_localizations\":null,\"contexts\":[],\"default_permission\":null,\"default_member_permissions\":null,\"dm_permission\":false,\"integration_types\":[],\"nsfw\":false}]");
```

### API v0

#### Getting a bot

##### Specific bot

```cs
var bot = await client.GetBotAsync(264811613708746752U);
```

##### Own bot

```cs
var bot = await client.GetMeAsync();
```

#### Getting several bots

##### With defaults

```cs
var bots = await client.GetBotsAsync();
```

##### With explicit arguments

```cs
//                                   Limit  Offset  Sort by
var bots = await client.GetBotsAsync(100,   1,      SortBotsBy.MonthlyPoints);
```

#### Getting your project's voters

##### First page

```cs
var voters = await client.GetVotersAsync();
```

##### Subsequent pages

```cs
//                                       Page number
var voters = await client.GetVotersAsync(2);
```

#### Check if a user has voted for your project

```cs
var voted = await client.HasVotedAsync(661200758510977084U);
```

#### Getting your bot's statistics

```cs
var stats = await client.GetBotStatsAsync();
```

#### Posting your bot's statistics

```cs
await client.UpdateStatsAsync(bot.GetServerCount());
```

#### Automatically posting your bot's statistics every few minutes

With Discord.NET:

```cs
var submissionAdapter = client.CreateListener();

submissionAdapter.Start();

// ...

submissionAdapter.Stop(); // Optional
```

#### Checking if the weekend vote multiplier is active

```cs
var isWeekend = await client.IsWeekendAsync();
```

#### Generating widget URLs

##### Large

```cs
var widgetUrl = Widget.Large(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

##### Votes

```cs
var widgetUrl = Widget.Votes(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

##### Owner

```cs
var widgetUrl = Widget.Owner(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

##### Social

```cs
var widgetUrl = Widget.Social(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

### Webhooks

#### Being notified whenever someone voted for your project

With ASP.NET Core or Blazor:

```cs
using DiscordBotsList.Api.Webhooks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var webhooks = new Webhooks(Environment.GetEnvironmentVariable("TOPGG_WEBHOOK_PASSWORD"));

app.MapPost("/webhooks", webhooks.Listener((context, vote) =>
{
  Console.WriteLine($"A user with the ID of {vote.VoterId} has voted us on Top.gg!");

  return Task.CompletedTask;
}));

app.Run();
```