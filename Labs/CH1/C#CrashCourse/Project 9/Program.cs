using System;
using System.Collections.Generic;

interface ITurnable
{
    void Turn();
}

class Page : ITurnable
{
    public void Turn()
    {
        Console.WriteLine("The page is being turned to reveal the next chapter.");
    }
}

class Corner : ITurnable
{
    public void Turn()
    {
        Console.WriteLine("The corner is being turned at a 90-degree angle.");
    }
}

class Pancake : ITurnable
{
    public void Turn()
    {
        Console.WriteLine("The pancake is being flipped over to cook the other side.");
    }
}

class Leaf : ITurnable
{
    public void Turn()
    {
        Console.WriteLine("The leaf is turning brown as autumn arrives.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<ITurnable> turnableItems = new List<ITurnable>();

        turnableItems.Add(new Page());
        turnableItems.Add(new Corner());
        turnableItems.Add(new Pancake());
        turnableItems.Add(new Leaf());

        Console.WriteLine("=== Turning Objects Demo ===\n");
        TurnAll(turnableItems);
    }

    static void TurnAll(List<ITurnable> items)
    {
        foreach (ITurnable item in items)
        {
            item.Turn();
        }
    }
}