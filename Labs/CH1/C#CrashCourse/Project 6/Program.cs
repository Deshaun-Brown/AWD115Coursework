using System;



int[] testScores = { 85, 92, 78, 95, 88, 76, 91, 83, 89, 94 };

int max = testScores[0];
int min = testScores[0];
int sum = 0;

foreach (int score in testScores)
{
    if (score > max)
    {
        max = score;
    }

    if (score < min)
    {
        min = score;
    }

    sum += score;
}

double average = (double)sum / testScores.Length;

Console.WriteLine("Test Scores Analysis:");
Console.WriteLine("--------------------");
Console.WriteLine($"Best Score:    {max}");
Console.WriteLine($"Worst Score:   {min}");
Console.WriteLine($"Average Score: {average:F2}");
Console.WriteLine($"Total Sum:     {sum}");

