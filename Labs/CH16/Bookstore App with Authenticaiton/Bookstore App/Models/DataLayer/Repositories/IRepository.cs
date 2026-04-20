namespace Bookstore_App.Models.DataLayer.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> List();
    Task<T?> GetAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
