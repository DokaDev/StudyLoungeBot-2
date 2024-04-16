using Discord.Interactions;

namespace ServerCore.Command.CommandGroup;

[SlashCommandGroup("GitHub")]
public class GitHubCommand : InteractionModuleBase<SocketInteractionContext> {
    [SlashCommand("Get", "Get GitHub Repository Information")]
    public async Task GetGitHubRepositoryAsync() {

    }
}