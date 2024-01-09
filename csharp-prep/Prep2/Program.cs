using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("\nWhat is your grade percentage? ");
        string userText = Console.ReadLine();
        string errorText = "Your input is incorrect. Try one more time.";
        if (int.TryParse(userText, out int userPercentage))
        {
            userPercentage = int.Parse(userText);
        }
        else
        {
            Console.WriteLine($"\n{errorText}");
            return;
        }
        
        string letter = "";
        int minToPass = 70;

        if (userPercentage >= 90)
        {
            letter = "A";
        }
        else if (userPercentage >= 80)
        {
            letter = "B";
        }
        else if (userPercentage >= 70)
        {
            letter = "C";
        }
        else if (userPercentage >= 60)
        {
            letter = "D";
        }
        else if (userPercentage < 60 && userPercentage >= 0)
        {
            letter = "F";
        }
        else
        {
            Console.WriteLine($"\n{errorText}");
            return;
        }

        int lastNumber = userPercentage % 10;
        string plusOrMinus = "";

        if (letter != "F")
        {
            if (letter != "A" && lastNumber >= 7)
            {
                plusOrMinus = "+";
            }
            else if (lastNumber < 3)
            {
                plusOrMinus = "-";
            }
        }

        Console.WriteLine($"\nYour letter grade is {letter}{plusOrMinus}");

        if (userPercentage >= minToPass)
        {
            Console.WriteLine("Congrats! You passed the class!");
        }
        else 
        {
            Console.WriteLine("You didn't pass the class. Wish you better luck next time!");
        }
    }
}