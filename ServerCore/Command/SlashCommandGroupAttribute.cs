namespace ServerCore.Command;

[AttributeUsage(AttributeTargets.Class)]
public class SlashCommandGroupAttribute(string? groupName) : Attribute {}