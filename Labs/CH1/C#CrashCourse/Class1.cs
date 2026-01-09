using System;




//Reecords are immytable by default.

//When to use records:
//1. When you want to test for value equality. 


string[] PhoneNumbers = new string[2];

	Person p1 = new Person("John", "Doe",);

	Console.WriteLine(p1.ToString());

public record Person(string FirstName, string LastName, string PhoneNumbers) 
{




}

