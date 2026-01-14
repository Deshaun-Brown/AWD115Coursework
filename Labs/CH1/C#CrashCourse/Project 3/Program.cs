using System;



class MainProgram
{
    static void Main(string[] args)
    {



        Console.Write("Enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Enter number of columns: ");
        int cols = int.Parse(Console.ReadLine());

        // Print header row
        Console.Write("     |");
        for (int c = 1; c <= cols; c++)
        {
            Console.Write($"{c,5}");
        }
        Console.WriteLine();

        // Print separator line
        Console.Write("-----+");
        for (int c = 1; c <= cols; c++)
        {
            Console.Write("-----");
        }
        Console.WriteLine();

        // Print table body
        for (int r = 1; r <= rows; r++)
        {
            Console.Write($"{r,5} |");
            for (int c = 1; c <= cols; c++)
            {
                Console.Write($"{r * c,5}");
            }
            Console.WriteLine();
        }

    }
}
      