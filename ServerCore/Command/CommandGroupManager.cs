using System.Reflection;
using Discord.Interactions;

namespace ServerCore.Command;

public class CommandGroupManager(CoreModule coreModule) {
    public async Task SetSlashCommandModulesAsync() {
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach(var type in assembly.GetTypes()) {
            if(type.GetCustomAttributes(typeof(SlashCommandGroupAttribute), true).Length > 0
               &&
               typeof(InteractionModuleBase<SocketInteractionContext>).IsAssignableFrom(type))
                await coreModule.InteractionService!.AddModuleAsync(type, null);
        }
    }
}