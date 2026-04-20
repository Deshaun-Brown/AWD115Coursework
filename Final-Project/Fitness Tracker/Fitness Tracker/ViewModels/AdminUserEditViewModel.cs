namespace Fitness_Tracker.ViewModels;

public class AdminUserEditViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> AllRoles { get; set; } = new();
    public List<string> SelectedRoles { get; set; } = new();
}
