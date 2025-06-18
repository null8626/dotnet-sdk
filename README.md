# Top.gg for .NET

The community-maintained .NET library for Top.gg.

## Usage

#### Setting up
```cs
DiscordBotListApi DblApi = new DiscordBotListApi(BOT_DISCORD_ID, YOUR_TOKEN);
```

#### Getting bots
```cs
//                                  discord id
IBot bot = await DblApi.GetBotAsync(160105994217586689);
```

#### Updating stats
```cs
IDblSelfBot me = await DblApi.GetMeAsync();

// Update stats           guildCount
await me.UpdateStatsAsync(2133);
```

#### Widgets
```cs
string widgetUrl = Widget.Large(WidgetType.DISCORD_BOT, 1026525568344264724);
```

Generates ![](https://top.gg/api/v1/widgets/large/discord/bot/1026525568344264724)

### Download

#### Nuget

If you're using Nuget, you can use find it with the ID `DiscordBotsList.Api` or use

```
Install-Package DiscordBotsList.Api
```
