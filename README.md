# Top.gg .NET SDK

The community-maintained .NET library for Top.gg.

## Installation

If you're using Nuget, you can use install it with the ID `DiscordBotsList.Api` like so:

```powershell
> Install-Package DiscordBotsList.Api
```

## Setting up

### Library agnostic

```cs
var dbl = new DiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

### Discord.NET-based

If you're using Nuget, you can use install it with the ID `DiscordBotsList.Api.Adapter.Discord.Net` like so:

```powershell
> Install-Package DiscordBotsList.Api.Adapter.Discord.Net
```

Then use it in your code:

```cs
var discordNetClient = ...;
var dbl = new DiscordNetDblApi(discordNetClient, "TOPGG_TOKEN");
```

## Usage

### Getting a bot

```cs
//                              Discord ID
var bot = await dbl.GetBotAsync(264811613708746752U);
```

### Getting several bots

```cs
//                                Sort by                   Count  Offset
var bots = await dbl.GetBotsAsync(SortBotsBy.MonthlyPoints, 100,   1);
var firstBot = bots.Items.First();
```

### Getting your bot's voters

First page:

```cs
var voters = await dbl.GetVotersAsync();
```

Following pages:

```cs
//                                    Page number
var voters = await dbl.GetVotersAsync(2);
```

### Check if a user has voted your bot

```cs
//                             Discord ID
var voted = await dbl.HasVoted(661200758510977084U);
```

### Getting your bot's server count

```cs
var serverCount = await dbl.GetServerCountAsync();
```

### Posting your bot's server count

```cs
//                               Server count
await dbl.UpdateServerCountAsync(2);
```

### Automatically posting your bot's server count every few minutes

For Discord.NET:

```cs
var submissionAdapter = dbl.CreateListener();

submissionAdapter.Start();

// ...

submissionAdapter.Stop(); // Optional
```

### Checking if the weekend vote multiplier is active

```cs
var isWeekend = await dbl.IsWeekendAsync();
```

### Generating widget URLs

```cs
//                            Widget type             Discord ID
var widgetUrl1 = Widget.Large(WidgetType.DISCORD_BOT, 1026525568344264724U);

//                            Widget type             Discord ID
var widgetUrl2 = Widget.Votes(WidgetType.DISCORD_BOT, 1026525568344264724U);

//                            Widget type             Discord ID
var widgetUrl3 = Widget.Owner(WidgetType.DISCORD_BOT, 1026525568344264724U);

//                             Widget type             Discord ID
var widgetUrl4 = Widget.Social(WidgetType.DISCORD_BOT, 1026525568344264724U);
```