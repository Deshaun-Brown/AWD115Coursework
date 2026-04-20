using Bookstore_App.Models.DomainModels;

namespace Bookstore_App.Models.DataLayer;

public static class QueryExtensions
{
    public static IQueryable<Book> SearchByTitle(this IQueryable<Book> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        return query.Where(b => b.Title.Contains(search));
    }
}
