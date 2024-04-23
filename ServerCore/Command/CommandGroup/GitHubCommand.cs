using Discord.Interactions;

namespace ServerCore.Command.CommandGroup;

[SlashCommandGroup("GitHub")]
public class GitHubCommand : InteractionModuleBase<SocketInteractionContext> {
    // [SlashCommand("Get", "Get GitHub Repository Information")]
    // public async Task GetGitHubRepositoryAsync() {
    //
    // }

    [SlashCommand("ping2", "test2")]
    public async Task PingTest() {
        await RespondAsync("Pong! #2");
    }
}