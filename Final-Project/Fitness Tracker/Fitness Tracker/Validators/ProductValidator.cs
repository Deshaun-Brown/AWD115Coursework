using FluentValidation;
using Fitness_Tracker.Models;
using Microsoft.EntityFrameworkCore;


namespace Fitness_Tracker.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Please enter a product name.")
                .MustAsync(BeUniqueName).WithMessage("A product with this name already exists.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Please enter a description.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Please enter a price.")
                .InclusiveBetween(1, 100000).WithMessage("Price must be between $1 and $100,000.");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Please enter a quantity.")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a positive number.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Please select a category.");
        }

        private async Task<bool> BeUniqueName(Product product, string name, CancellationToken cancellationToken)
        {
            // If the name is null/empty, we'll let NotEmpty handle it
            if (string.IsNullOrWhiteSpace(name)) return true;

            // Look for any OTHER product in the database that has the same name
            return !await _context.Products
                .AnyAsync(p => p.Name.ToLower() == name.ToLower() && p.ProductId != product.ProductId, cancellationToken);
        }
    }
}
