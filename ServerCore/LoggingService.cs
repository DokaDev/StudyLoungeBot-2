using Discord;
using Discord.Commands;
using Discord.Interactions;

namespace ServerCore;

public class LoggingService {
    public CoreModule CoreModule { get; } // Dependency Injection

    /// <summary>
    ///
    /// </summary>
    /// <param name="coreModule">Main context of the bot</param>
    public LoggingService(CoreModule coreModule) {
        CoreModule = coreModule;
        Task.Run(SetEventHandler); // Not async
    }

    private Task SetEventHandler() {
        CoreModule.DiscordSocketClient!.Log += LogAsync;
        CoreModule.CommandService!.Log += LogAsync;

        // CoreModule.Intera

        return Task.CompletedTask;
    }

    public async Task PrintMessageAsync(string msg) {
        await Console.Out.WriteLineAsync(msg);
    }

    private async Task LogAsync(LogMessage msg) {
        if(msg.Exception is CommandException cmdException) {
            await PrintMessageAsync($"[Command/{msg.Severity}] {cmdException.Command.Aliases.First()} failed to execute in {cmdException.Context.Channel}.");
            await PrintMessageAsync($"{cmdException}");
        } else await PrintMessageAsync($"[General/{msg.Severity}] {msg}");
    }

    private async Task SlashCommandExecutedAsync(SlashCommandInfo cmdInfo, IInteractionContext context, Discord.Interactions.IResult result) {
        if(!result.IsSuccess) await PrintMessageAsync($"[Command/{LogSeverity.Error}] {cmdInfo.CommandType} failed to execute in {context.Channel}.");
        else await PrintMessageAsync($"[Command/{LogSeverity.Info}] {cmdInfo.CommandType} executed in {context.Channel}.");
    }
}