using System.Linq.Expressions;

namespace Chapter_8_1_Student_Project.Data;

public class QueryOptions<T>
{
    public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
    public string OrderByDirection { get; set; } = "asc";
    public Expression<Func<T, bool>> Where { get; set; } = null!;

    private string[] includes = Array.Empty<string>();
    public string Includes
    {
        set => includes = value.Replace(" ", "").Split(',');
    }

    public string[] GetIncludes() => includes;

    public bool HasWhere => Where != null;
    public bool HasOrderBy => OrderBy != null;
}