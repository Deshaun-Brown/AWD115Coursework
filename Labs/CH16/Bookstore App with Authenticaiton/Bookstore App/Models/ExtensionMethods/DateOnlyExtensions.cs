namespace Bookstore_App.Models.ExtensionMethods;

public static class DateOnlyExtensions
{
    public static string ToShortIso(this DateOnly value) => value.ToString("yyyy-MM-dd");
}
