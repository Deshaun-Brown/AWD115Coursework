using System;

int[] testScores = { 85, 90, 80, 60, 76, 89, 54, 44, 39, 98, 10 };

double max = double.MinValue;
double min = double.MaxValue;
int sum = 0;

foreach (int orange in testScores)
{ 

    if (orange > max)
    {

        max = orange;
    }

    if (orange < min)
    {


        min = orange;//this prints the lowest score/ number

    }

    sum += orange;

}




    double average = (double)sum / testScores.Length;

Console.WriteLine("Test Scores Anaylsis:");
Console.WriteLine("-----------------");
Console.WriteLine($"Best Score: {max}");
Console.WriteLine($"Worse Score: {min}");
Console.WriteLine($"Average Score: {average:F2}");
Console.WriteLine($"Total Sum: {sum}");
