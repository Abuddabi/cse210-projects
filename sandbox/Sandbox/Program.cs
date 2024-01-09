using System;

class Program
{
    static void Main(string[] args)
    {
        string userText = Console.ReadLine();
        bool test = int.TryParse(userText, out int guessNumber);

        Console.WriteLine(guessNumber);
    }
}