namespace Bookstore_App.Models.DataLayer;

public class QueryOptions
{
    public string? Search { get; set; }
    public string SortField { get; set; } = string.Empty;
    public string SortDirection { get; set; } = "asc";
}
