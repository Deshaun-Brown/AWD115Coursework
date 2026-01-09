// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



//Ask the user for their name
Console.WriteLine("Please enter your name");

string name = Console.ReadLine();

Console.WriteLine("please enter your age"); 
//Ask the user for their age, append their age to the WriteLine Statment in line 34.

int userAge = Convert.ToInt32(Console.ReadLine());

//Hello Bob you are 43 years old.
Console.WriteLine($"Hello {name} you are {userAge} years old");


Console.WriteLine($"Please enter your favorite video game:");

  string game =  Console.ReadLine();

Console.WriteLine($"Please enter your favorite game genre:");

string  genre = Console.ReadLine();

Console.WriteLine($"Your favorite video game is {game} and {genre} ");

