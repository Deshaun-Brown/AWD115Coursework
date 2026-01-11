using System;

Console.Write("Enter a credit card number: ");
string cardNumber = Console.ReadLine();

string maskedCard = "";

// Calculate how many characters to mask (all digits/letters except last 4)
int digitCount = 0;
foreach (char c in cardNumber)
{
    if (Char.IsDigit(c) || Char.IsLetter(c))
    {
        digitCount++;
    }
}

int digitsToMask = digitCount - 4;
int maskedCount = 0;

// Process each character
foreach (char c in cardNumber)
{
    if (Char.IsDigit(c) || Char.IsLetter(c))
    {
        if (maskedCount < digitsToMask)
        {
            maskedCard += "X";
            maskedCount++;
        }
        else
        {
            maskedCard += c;
        }
    }
    else
    {
        // Keep spaces and dashes as is
        maskedCard += c;
    }
}

Console.WriteLine($"Masked card: {maskedCard}");