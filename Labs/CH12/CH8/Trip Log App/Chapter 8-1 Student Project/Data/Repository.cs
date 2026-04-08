using Microsoft.EntityFrameworkCore;

namespace Chapter_8_1_Student_Project.Data;

public class Repository<T> : IRepository<T> where T : class
{



    protected TripsContext context;
    private DbSet<T> dbset;

    public Repository(TripsContext ctx)
    {
        context = ctx;
        dbset = context.Set<T>();
    }

    public virtual IEnumerable<T> List(QueryOptions<T> options)
    {
        IQueryable<T> query = dbset;
        foreach (string include in options.GetIncludes())
        {
            query = query.Include(include);
        }
        if (options.HasWhere)
        {
            query = query.Where(options.Where);
        }
        if (options.HasOrderBy)
        {
            if (options.OrderByDirection == "asc")
                query = query.OrderBy(options.OrderBy);
            else
                query = query.OrderByDescending(options.OrderBy);
        }
        return query.ToList();
    }

    public virtual T? Get(int id) => dbset.Find(id);

    public virtual void Insert(T entity) => dbset.Add(entity);

    public virtual void Update(T entity) => dbset.Update(entity);

    public virtual void Delete(T entity) => dbset.Remove(entity);

    public virtual void Save() => context.SaveChanges();
}