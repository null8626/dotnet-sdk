# Top.gg .NET SDK

The community-maintained .NET library for Top.gg.

## Chapters

- [Installation](#installation)
- [Setting up](#setting-up)
- [Usage](#usage)
  - [Getting your project's information](#getting-your-projects-information)
  - [Getting your project's vote information of a user](#getting-your-projects-vote-information-of-a-user)
  - [Posting your bot's application commands list](#posting-your-bots-application-commands-list)
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

```cs
var client = new AuthDiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

### Discord.NET-based

```cs
var discordNetClient = ...;
var client = new DiscordNetDblApi(discordNetClient, "TOPGG_TOKEN");
```

## Usage

### Getting your project's information

```cs
var project = await client.GetSelfAsync();
```

### Getting your project's vote information of a user

```cs
var vote = await client.GetVoteAsync(661200758510977084);
```

### Posting your bot's application commands list

```cs
// Array of application commands in Discord API's raw JSON format.
await client.UpdateCommandsAsync("[{\"options\":[],\"name\":\"test\",\"name_localizations\":null,\"description\":\"command description\",\"description_localizations\":null,\"contexts\":[],\"default_permission\":null,\"default_member_permissions\":null,\"dm_permission\":false,\"integration_types\":[],\"nsfw\":false}]");
```

### Generating widget URLs

#### Large

```cs
var widgetUrl = Widget.Large(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Votes

```cs
var widgetUrl = Widget.Votes(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Owner

```cs
var widgetUrl = Widget.Owner(WidgetType.DISCORD_BOT, 1026525568344264724U);
```

#### Social

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
