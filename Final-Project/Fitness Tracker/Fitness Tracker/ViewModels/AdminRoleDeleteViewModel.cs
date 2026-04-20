namespace Fitness_Tracker.ViewModels;

public class AdminRoleDeleteViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Users { get; set; } = new();
}
