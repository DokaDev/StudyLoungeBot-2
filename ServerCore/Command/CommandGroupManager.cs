using System.Reflection;
using Discord.Interactions;
using ServerCore.Command.CommandGroup;

namespace ServerCore.Command;

public class CommandGroupManager(CoreModule coreModule) {
    // public List<>
    public async Task SetSlashCommandModulesAsync() {
        if (coreModule.InteractionService == null) {
            Console.WriteLine("InteractionService is not initialized.");
            return;
        }

        Assembly assembly = Assembly.GetExecutingAssembly();
        Console.WriteLine("Set Slash Command Modules");

        foreach (var type in assembly.GetTypes()) {
            if (type.IsSubclassOf(typeof(InteractionModuleBase<>)) &&
                type.GetCustomAttributes(typeof(SlashCommandGroupAttribute), true).Length > 0) {
                Console.WriteLine($"Adding Slash Command Module: {type.Name}");
                try {
                    await coreModule.InteractionService!.AddModuleAsync(type, null);
                    // typeof(type) -> class: type
                } catch (Exception ex) {
                    Console.WriteLine($"Failed to add module {type.Name}: {ex.Message}");
                }
            }
        }
    }
}