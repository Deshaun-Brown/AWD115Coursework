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
    Task<List<CartItem>> GetCartItemsForUserAsync(string userId);
    Task AddToCartAsync(string userId, int productId, int quantity = 1);
    Task RemoveCartItemAsync(int cartItemId);

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

    public async Task<List<CartItem>> GetCartItemsForUserAsync(string userId)
    {
        return await _db.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();
    }

    public async Task AddToCartAsync(string userId, int productId, int quantity = 1)
    {
        var existing = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        if (existing != null)
        {
            existing.Quantity += quantity;
            _db.CartItems.Update(existing);
        }
        else
        {
            await _db.CartItems.AddAsync(new CartItem { UserId = userId, ProductId = productId, Quantity = quantity });
        }
        await _db.SaveChangesAsync();
    }

    public async Task RemoveCartItemAsync(int cartItemId)
    {
        var item = await _db.CartItems.FindAsync(cartItemId);
        if (item != null)
        {
            _db.CartItems.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
