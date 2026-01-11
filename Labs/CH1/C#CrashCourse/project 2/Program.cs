using System;

double N = double.Parse(Console.ReadLine());


for (int x = 1; x <= N; x++)
{
    
    Console.WriteLine($"The current value of x is {x}");

}

Console.Write("Enter an upper limit (N): ");

for (int x = 1; x <= N; x++)
{
    if (x % 15 == 0)
    {
        Console.WriteLine("FizzBuzz");
    }
    else if (x % 3 == 0)
    {
        Console.WriteLine("Fizz");
    }
    else if (x % 5 == 0)
    {
        Console.WriteLine("Buzz");
    }
    else
    {
        Console.WriteLine(x);
    }
}

