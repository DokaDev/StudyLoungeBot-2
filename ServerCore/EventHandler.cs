using Discord;
using Discord.WebSocket;

namespace ServerCore;

public class EventHandler(CoreModule coreModule) {
    public async Task ReadyAsync() {
        if(coreModule.DiscordSocketClient is null) return;
        if(coreModule.InteractionService is null) return;

        await Console.Out.WriteLineAsync("Bot is ready to serve");
        if(coreModule.DiscordSocketClient.ShardId == 0) {
            await coreModule.InteractionService.RegisterCommandsToGuildAsync(AuthorizeKey.Guild.TestServer.GuildId);
            await coreModule.InteractionService.RegisterCommandsGloballyAsync();

            // todo. update this to use the new command group manager
        }
    }

    public async Task MessageReceivedAsync(SocketMessage msg) {
        if(coreModule.DiscordSocketClient is null) return;
        if(msg.Author.Id == coreModule.DiscordSocketClient.CurrentUser.Id) return; // Ignore self(bot)

        await Console.Out.WriteLineAsync($"[Message/{msg.Channel.Id}] {msg.Author.Username}: {msg.Content}");
    }

    public async Task MessageUpdatedAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
        // If the message was not in the cache, downloading it will result in getting a copy of the message.
        IMessage msg = await before.GetOrDownloadAsync();
        Console.WriteLine($"{msg} -> {after}");
    }
}