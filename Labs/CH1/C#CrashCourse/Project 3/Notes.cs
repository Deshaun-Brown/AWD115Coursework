//using System.Collections.Generic;
//using System;

////at the start of this file I learned that I have too use a class for this file in the project
////because C# auto initializes a Hidden Main method in the first file
////when you run the project which allows no need for a Main method in that file
////

//public class NotesExamples
//{
//    public static void Notes()
//    {
//        Console.Write("Enter number of rows: ");
//        int rows = int.Parse(Console.ReadLine());

//        Console.WriteLine("Enter number of columns: ");
//        int cols = int.Parse(Console.ReadLine());

//        //Print header row
//        Console.Write("      |");
//        for (int c = 1; c <= cols; c++)
//        {
//            Console.Write($"{c,5} |");
//        }
//        Console.WriteLine();

//        // Print separator
//        for (int c = 1; c <= cols; c++)
//        {
//            Console.Write("- - - - - ");
//        }
//        Console.WriteLine();

//        // Print table body
//        for (int r = 1; r <= rows; r++)
//        {
//            Console.Write($"{r,5} |");
//            for (int c = 1; c <= cols; c++)
//            {
//                Console.Write($"{r * c,5} |");
//            }
//            Console.WriteLine();
//        }
//    }


//}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        NotesExamples.Notes();
//    }
//}




