using Fitness_Tracker.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data;

public interface IDatabaseAgent
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<int> GetProductCountAsync();
    Task<List<Fitness_Tracker.Models.CartItem>> GetCartItemsForUserAsync(string userId);
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

    public async Task<List<Fitness_Tracker.Models.CartItem>> GetCartItemsForUserAsync(string userId)
    {
        return await _db.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();
    }
}
