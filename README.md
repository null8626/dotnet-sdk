# DBL-dotnet-Library
top.gg botlist wrapper

## Usage
#### Setting up
```cs
DiscordBotListApi DblApi = new DiscordBotListApi(BOT_DISCORD_ID, YOUR_TOKEN);
```

#### Getting bots
```cs
//                            discord id
IBot bot = DblApi.GetBotAsync(160105994217586689);
```

#### Updating stats
```cs
IDblSelfBot me = await DblApi.GetMeAsync();

// Update stats           guildCount
await me.UpdateStatsAsync(2133);
```

#### Widgets
```cs
string widgetUrl = Widget.Large(1026525568344264724);
```

Generates ![](https://top.gg/api/v1/widgets/large/1026525568344264724)

### Download
#### Nuget
If you're using Nuget you can use find it with the ID `DiscordBotsList.Api` or use
> Install-Package DiscordBotsList.Api
