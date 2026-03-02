using Fitness_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data;

public interface IDatabaseAgent
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<int> GetProductCountAsync();
}

public class DatabaseAgent : IDatabaseAgent
{
    private readonly ApplicationDbContext _db;

    public DatabaseAgent(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _db.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _db.Products.Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<int> GetProductCountAsync()
    {
        return await _db.Products.CountAsync();
    }
}
