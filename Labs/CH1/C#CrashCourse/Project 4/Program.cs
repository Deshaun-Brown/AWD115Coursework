using System;


string original = "Hello World";
string reversed = "";

for (int i = original.Length - 1; i >= 0; i--)
{
    reversed += original[i];
}
Console.WriteLine("Original: " + original);
Console.WriteLine("Reversed: " + reversed);
