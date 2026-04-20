using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Models.DataLayer.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _set;
    private readonly BookstoreContext _context;

    public EfRepository(BookstoreContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public IQueryable<T> List() => _set;

    public Task<T?> GetAsync(int id) => _set.FindAsync(id).AsTask();

    public async Task AddAsync(T entity)
    {
        _set.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
