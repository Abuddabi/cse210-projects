using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        string userText;
        int userNumber;
        bool isInteger;
        string errorText = "Your input is incorrect. To stop enter '0'. Try one more time.";
        List<int> numbers = new List<int>();
        List<int> positiveNumbers = new List<int>();

        do
        {
            Console.Write("Enter number: ");
            userText = Console.ReadLine();
            isInteger = int.TryParse(userText, out userNumber);

            if (!isInteger)
            {
                Console.WriteLine(errorText);
                continue;
            }

            if (userNumber != 0)
            {
                numbers.Add(userNumber);
                if (userNumber > 0)
                {
                    positiveNumbers.Add(userNumber);
                }
            }
        } while (userNumber != 0 || !isInteger);

        if (numbers.Count == 0)
        {
            return;
        }

        numbers.Sort();
        int sum = numbers.Sum();
        double avg = numbers.Average();
        int max = numbers.Max();
        int min = numbers.Min();
        int minPositive = positiveNumbers.Min();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {avg}");
        Console.WriteLine($"The largest number is: {max}");
        Console.WriteLine($"The smallest positive number: {minPositive}");
        Console.WriteLine($"The smallest number is: {min}");
        Console.WriteLine($"The sorted list is: ");

        foreach (int i in numbers)
        {
            Console.WriteLine(i);
        }
    }
}