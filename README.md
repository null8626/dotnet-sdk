# Top.gg .NET SDK

The community-maintained .NET library for Top.gg.

## Chapters

- [Installation](#installation)
- [Setting up](#setting-up)
- [Usage](#usage)
  - [Getting your project's information](#getting-your-projects-information)
  - [Getting your project's vote information of a user](#getting-your-projects-vote-information-of-a-user)
  - [Getting a cursor-based paginated list of votes for your project](#getting-a-cursor-based-paginated-list-of-votes-for-your-project)
  - [Posting your bot's application commands list](#posting-your-bots-application-commands-list)
  - [Generating widget URLs](#generating-widget-urls)
  - [Webhooks](#webhooks)

## Installation

### Main API wrapper

```console
$ dotnet add package DiscordBotsList.Api --version 2.0.0
```

### Webhooks only

```console
$ dotnet add package DiscordBotsList.Webhooks --version 2.0.0
```

## Setting up

```cs
using DiscordBotsList.Api;

var client = new DiscordBotListApi(Environment.GetEnvironmentVariable("TOPGG_TOKEN"));
```

## Usage

### Getting your project's information

```cs
var project = await client.GetSelfAsync();
```

### Getting your project's vote information of a user

#### Discord ID

```cs
using DiscordBotsList.Api.Data;

var vote = await client.GetVoteAsync(661200758510977084, UserSource.Discord);
```

#### Top.gg ID

```cs
using DiscordBotsList.Api.Data;

var vote = await client.GetVoteAsync(8226924471638491136, UserSource.Topgg);
```

### Getting a cursor-based paginated list of votes for your project

```cs
var firstPage = await client.GetVotesAsync(DateTime.Now);

foreach (var vote in firstPage.Votes)
{
    // ...
}

var secondPage = await firstPage.Next();

foreach (var vote in secondPage.Votes)
{
    // ...
}
```

### Posting your bot's application commands list

```cs
// Array of application commands that
// can be serialized to Discord API's raw JSON format.
var commands = @"[
  {
    ""options"": [],
    ""name"": ""test"",
    ""name_localizations"": null,
    ""description"": ""command description"",
    ""description_localizations"": null,
    ""contexts"": [],
    ""default_permission"": null,
    ""default_member_permissions"": null,
    ""dm_permission"": false,
    ""integration_types"": [],
    ""nsfw"": false
  }
]";

await client.PostCommandsAsync(commands);
```

### Generating widget URLs

#### Large

```cs
using DiscordBotsList.Api.Data;
using DiscordBotsList.Api;

var widgetUrl = Widget.Large(ProjectType.DiscordBot, 574652751745777665);
```

#### Votes

```cs
using DiscordBotsList.Api.Data;
using DiscordBotsList.Api;

var widgetUrl = Widget.Votes(ProjectType.DiscordBot, 574652751745777665);
```

#### Owner

```cs
using DiscordBotsList.Api.Data;
using DiscordBotsList.Api;

var widgetUrl = Widget.Owner(ProjectType.DiscordBot, 574652751745777665);
```

#### Social

```cs
using DiscordBotsList.Api.Data;
using DiscordBotsList.Api;

var widgetUrl = Widget.Social(ProjectType.DiscordBot, 574652751745777665);
```

### Webhooks

```cs
using DiscordBotsList.Webhooks;

public class CustomWebhooks() : Webhooks(Environment.GetEnvironmentVariable("TOPGG_WEBHOOK_SECRET"))
{
    // Optional
    public override Task OnIntegrationCreate(HttpContext context, IntegrationCreatePayload payload, string trace) => DefaultResponse(context);

    // Optional
    public override Task OnIntegrationDelete(HttpContext context, IntegrationDeletePayload payload, string trace) => DefaultResponse(context);

    // Optional
    public override Task OnTest(HttpContext context, TestPayload payload, string trace) => DefaultResponse(context);

    // Optional
    public override Task OnVoteCreate(HttpContext context, VoteCreatePayload payload, string trace) => DefaultResponse(context);

    private static async Task DefaultResponse(HttpContext context)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 200;
        }
    }
}
```