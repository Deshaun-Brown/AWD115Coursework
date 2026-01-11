using System;
using System.Collections.Generic;

class Cart
{
    private string _cartID;
    private Dictionary<string, double> _items;

    public Cart(string cartID)
    {
        _cartID = cartID;
        _items = new Dictionary<string, double>();
    }

    public void AddItem(string itemName, double price)
    {
        if (_items.ContainsKey(itemName))
        {
            Console.WriteLine($"'{itemName}' is already in the cart. Updating price to ${price:F2}");
            _items[itemName] = price;
        }
        else
        {
            _items.Add(itemName, price);
            Console.WriteLine($"'{itemName}' added to cart for ${price:F2}");
        }
    }

    public void RemoveItem(string itemName)
    {
        if (_items.Remove(itemName))
        {
            Console.WriteLine($"'{itemName}' removed from cart.");
        }
        else
        {
            Console.WriteLine($"'{itemName}' not found in cart.");
        }
    }

    public double GetTotal()
    {
        double total = 0;
        foreach (var item in _items)
        {
            total += item.Value;
        }
        return total;
    }

    public override string ToString()
    {
        string result = $"Cart ID: {_cartID}\n";
        result += "Items in Cart:\n";
        result += "--------------------\n";

        if (_items.Count == 0)
        {
            result += "Cart is empty.\n";
        }
        else
        {
            foreach (var item in _items)
            {
                result += $"{item.Key}: ${item.Value:F2}\n";
            }
            result += "--------------------\n";
            result += $"Total: ${GetTotal():F2}";
        }

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Cart myCart = new Cart("CART001");

        Console.WriteLine("=== Shopping Cart Demo ===\n");

        myCart.AddItem("Lollipop", 2.50);
        myCart.AddItem("Chocolate Bar", 1.75);
        myCart.AddItem("Gummy Bears", 3.25);
        myCart.AddItem("Soda", 1.99);

        Console.WriteLine("\n" + myCart.ToString());

        Console.WriteLine("\n--- Removing Soda ---");
        myCart.RemoveItem("Soda");

        Console.WriteLine("\n" + myCart.ToString());

        Console.WriteLine($"\nCart Total: ${myCart.GetTotal():F2}");
    }
}