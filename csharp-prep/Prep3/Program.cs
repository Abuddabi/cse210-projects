using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new();
        int number = randomGenerator.Next(1, 11);
        string userText;
        int guessNumber;
        int guessCount = 0;
        bool isInteger;
        string errorText = "Your input is incorrect. Try one more time.";

        do
        {
            Console.Write("What is your guess? ");
            userText = Console.ReadLine();
            isInteger = int.TryParse(userText, out guessNumber);
            if (!isInteger)
            {
                Console.WriteLine(errorText);
                continue;
            }
            guessCount++;

            if (guessNumber < number)
            {
                Console.WriteLine("Higher");
            }
            else if (guessNumber > number)
            {
                Console.WriteLine("Lower");
            }
            else if (guessNumber == number)
            {
                Console.WriteLine("\nYou guessed it!");
                Console.WriteLine($"You made {guessCount} guesses.");

                Console.Write("\nDo you want to play again? ");
                userText = Console.ReadLine();

                if (userText == "yes")
                {
                    number = randomGenerator.Next(1, 11);
                    guessNumber = 0;
                    guessCount = 0;
                }
            }
        } while (guessNumber != number);        
    }
}