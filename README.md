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
var client = new DiscordBotListApi(DISCORD_ID, "TOPGG_TOKEN");
```

### Discord.NET-based

If you're using Nuget, you can use install it with the ID `DiscordBotsList.Api.Adapter.Discord.Net` like so:

```powershell
> Install-Package DiscordBotsList.Api.Adapter.Discord.Net
```

Then use it in your code:

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