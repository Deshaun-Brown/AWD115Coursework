using System.Collections.Generic;

namespace Fitness_Tracker.ViewModels
{
    public class PortfolioProject
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string GitHubUrl { get; set; } = string.Empty;
        public string? LiveDemoUrl { get; set; }
        public List<string> Technologies { get; set; } = new();
    }

    public class PortfolioViewModel
    {
        public List<PortfolioProject> Projects { get; set; } = new();
        public string GitHubUsername { get; set; } = string.Empty;
        public string GitHubProfileUrl { get; set; } = string.Empty;
    }
}
