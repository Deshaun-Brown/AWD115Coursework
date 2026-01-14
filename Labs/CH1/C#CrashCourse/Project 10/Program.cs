using System;


class Program
{
    static void Main()
    {
        var item1 = new Product("Widget", "W-123", 100.00m, new ShippingDimensions(10, 5, 2));
        var item2 = new Product("Widget", "W-123", 100.00m, new ShippingDimensions(10, 5, 2));

        Console.WriteLine($"Equality Test: {item1 == item2}");

        var discountedItem = item1 with { Price = item1.Price * 0.8m };

        Console.WriteLine($"Original Item: {item1}");
        Console.WriteLine($"disconted Item: {discountedItem}");

        var (name, sku, price, dims) = item1;
        Console.WriteLine($"Deconstructed SKU: {sku}");

        //Uncomment to see compiler errors:
        //item1.Price = 10.99m;
        //dims.Length = 15.0;


    }
}

public record Product(string Name, string Sku, decimal Price, ShippingDimensions Dimensions);

public readonly record struct ShippingDimensions(double Length, double Width, double Height);