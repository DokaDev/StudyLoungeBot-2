using AuthorizeKey;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace ServerCore;

public class CoreModule {
    public DiscordSocketClient? DiscordSocketClient { get; private set; }

    public DiscordSocketConfig? DiscordSocketConfig { get; } = new() {
        MessageCacheSize = 100,
        GatewayIntents = GatewayIntents.AllUnprivileged |
                         GatewayIntents.MessageContent,
        LogLevel = LogSeverity.Debug,
        AlwaysDownloadUsers = true
    };

    public CommandService? CommandService { get; private set; }
    public LoggingService? LoggingService { get; private set; }
    public InteractionService? InteractionService { get; private set; }

    public EventHandler? EventHandler { get; private set; }
    public IServiceProvider? ServiceProvider { get; private set; }

    public bool IsRunning { get; private set; } = false;    // Running Flag

    public CoreModule() {
        DiscordSocketClient = new(DiscordSocketConfig);

        CommandService = new();
        InteractionService = new(DiscordSocketClient);
        LoggingService = new(this);
        EventHandler = new(this);

        ConfigureServices();
    }

    public async Task? StartServeAsync() {
        if(DiscordSocketClient is null) throw new InvalidOperationException("DiscordSocketClient is null");
        if(CommandService is null) throw new InvalidOperationException("CommandService is null");
        if(LoggingService is null) throw new InvalidOperationException("LoggingService is null");
        if(InteractionService is null) throw new InvalidOperationException("InteractionService is null");
        if(EventHandler is null) throw new InvalidOperationException("EventHandler is null");

        await DiscordSocketClient.LoginAsync(TokenType.Bot, BotServer.Key);
        await DiscordSocketClient.StartAsync();

        DiscordSocketClient.MessageUpdated += EventHandler.MessageUpdatedAsync;
        DiscordSocketClient.MessageReceived += EventHandler.MessageReceivedAsync;
        DiscordSocketClient.Ready += EventHandler.ReadyAsync;
        DiscordSocketClient.InteractionCreated += async (interaction) => {
            var ctx = new SocketInteractionContext(DiscordSocketClient, interaction);
            await InteractionService.ExecuteCommandAsync(ctx, null);
        };
    }

    private void ConfigureServices() {

    }
}