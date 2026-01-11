using System;

// Step 2: Instantiate two separate Product objects with identical values
Product item1 = new Product("Widget", "W-123", 100.00m);
Product item2 = new Product("Widget", "W-123", 100.00m);

// Step 3: Print equality test - records use value equality
Console.WriteLine($"Equality Test: {item1 == item2}");

// Step 4: Create a discounted item using 'with' expression (20% off)
Product discountedItem = item1 with { Price = item1.Price * 0.80m };

// Step 5: Print both items - automatic ToString() formatting
Console.WriteLine($"Original Item: {item1}");
Console.WriteLine($"Discounted Item: {discountedItem}");

// Step 6: Re-instantiate item1 with ShippingDimensions
ShippingDimensions dims = new ShippingDimensions(10.0, 5.0, 3.0);
item1 = new Product("Widget", "W-123", 100.00m, dims);

// Step 7: Deconstruct item1 into individual variables
var (name, sku, price, dimensions) = item1;
Console.WriteLine($"Deconstructed SKU: {sku}");

// Step 8: Attempting to modify immutable properties (both will cause compiler errors)
// Uncomment to see compiler errors:

// The Record (Class): Cannot modify - records are immutable by default
// item1.Price = 10.99m; // Error: Init-only property can only be assigned in object initializer

// The Record Struct: Cannot modify - record structs are also immutable by default
// dims.Length = 15.0; // Error: Init-only property can only be assigned in object initializer


// Step 1: Define the records at the bottom using positional syntax
record Product(string Name, string Sku, decimal Price, ShippingDimensions Dimensions);

record struct ShippingDimensions(double Length, double Width, double Height);