using System.Reflection;

namespace ServerCore.Command;

public class CommandGroupManager(CoreModule coreModule) {
    public async Task LoadCommandAsync() {
        Assembly asm = Assembly.GetEntryAssembly();
        List<Type> moduleTypes = asm.GetTypes()
            .Where(x => x.GetCustomAttributes<SlashCommandGroupAttribute>(true).Any())
            .ToList();

        foreach (var type in moduleTypes) {
            var attribute = type.GetCustomAttributes<SlashCommandGroupAttribute>();
            Console.WriteLine($"Loading {attribute} group: {type.Name}");
        }
    }
}