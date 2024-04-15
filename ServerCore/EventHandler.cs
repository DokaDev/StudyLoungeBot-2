using Discord;
using Discord.WebSocket;

namespace ServerCore;

public class EventHandler {
    public CoreModule CoreModule { get; }

    public EventHandler(CoreModule coreModule) {    // Dependency Injection
        CoreModule = coreModule;
    }

    public async Task ReadyAsync() {
        if(CoreModule.DiscordSocketClient is null) return;
        if(CoreModule.InteractionService is null) return;

        await Console.Out.WriteLineAsync("Bot is ready to serve");

        // await AuthorizeKey.Guild.TestServer.GuildId;
        await CoreModule.InteractionService.RegisterCommandsToGuildAsync(AuthorizeKey.Guild.TestServer.GuildId);
        await CoreModule.InteractionService.RegisterCommandsGloballyAsync();
    }

    public async Task MessageReceivedAsync(SocketMessage msg) {
        if(CoreModule.DiscordSocketClient is null) return;
        if(msg.Author.Id == CoreModule.DiscordSocketClient.CurrentUser.Id) return; // Ignore self(bot)

        await Console.Out.WriteLineAsync($"[Message/{msg.Channel.Id}] {msg.Author.Username}: {msg.Content}");
    }

    public async Task MessageUpdatedAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
        // If the message was not in the cache, downloading it will result in getting a copy of the message.
        IMessage msg = await before.GetOrDownloadAsync();
        Console.WriteLine($"{msg} -> {after}");
    }


}