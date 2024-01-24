/* 
  I added API request to https://api.esv.org
  so the user can choose any verse from the Bible.
 */

using System;
using Microsoft.Extensions.Configuration;

class Program
{
  private static bool _onlineMode = false;
  private static readonly string _incorrectInputMsg = "Your input is incorrect. Try one more time.";

  static async Task Main(string[] args)
  {
    string userInput;
    bool inputValid;

    Console.Clear();

    do
    {
      Console.Write("Do you want to use The English Standard Version (ESV) Bible online? [yes/no]: ");
      userInput = Console.ReadLine();
      inputValid = true;

      if (userInput == "yes")
      {
        _onlineMode = true;
      }
      else if (userInput == "no")
      {
        break;
      }
      else
      {
        inputValid = false;
        Console.WriteLine(_incorrectInputMsg);
      }
    } while (!inputValid);

    string[] theVerse;
    if (_onlineMode)
    {
      theVerse = await GetVerseOnline();
    }
    else
    {
      theVerse = GetDefaultVerse();
    }

    string reference = theVerse[0];
    string verseText = theVerse[1];
    Reference referenceObj = GetReferenceFromString(reference);

    Scripture scripture = new Scripture(referenceObj, verseText);
    Console.Clear();

    bool canContinue;
    do
    {
      Console.WriteLine(scripture.GetDisplayText());

      canContinue = GetUserAnswer();
      // additionally check if all words are hidden
      canContinue = canContinue && !scripture.IsCompletelyHidden();

      if (canContinue)
      {
        scripture.HideRandomWords();
        Console.Clear();
      }
    } while (canContinue);
  }

  private static async Task<string[]> GetVerseOnline()
  {
    string apiKey = GetConfigValue("API_KEYS:ESV");

    if (apiKey == null)
    {
      Console.WriteLine("Please paste your API key or type 'cancel':");
      string userInput = Console.ReadLine();

      if (userInput == "cancel")
      {
        return GetDefaultVerse();
      }

      apiKey = userInput;
    }

    string reference;
    bool isValid;
    do
    {
      Console.WriteLine("Please write the reference to the scripture from Bible you want to memorize: \n(For example: \"Proverbs 3:5-6\")");
      reference = Console.ReadLine();
      isValid = reference.Contains(' ') && reference.Contains(':');

      if (!isValid)
      {
        Console.WriteLine(_incorrectInputMsg);
      }
    } while (!isValid);

    BibleAPIHandler apiHandler = new BibleAPIHandler(apiKey);

    Console.WriteLine("\nLoading... Please wait.");
    string result = await apiHandler.GetVerseByReference(reference);

    if (!apiHandler.IsSuccess())
    {
      Console.WriteLine($"{result}\nWe will continue with default verse. Press any key to continue.");
      Console.ReadKey();
      return GetDefaultVerse();
    }
    else
    {
      string[] verseWithReference = { reference, result };
      return verseWithReference;
    }
  }

  private static Reference GetReferenceFromString(string refString)
  {
    Reference reference;

    string[] parts = refString.Split(' ');
    string book = string.Join(" ", parts[0..^1]);

    // Extract chapter and verse information
    parts = parts[^1].Split(':');
    int chapter = int.Parse(parts[0]);
    string verses = parts[1];

    if (verses.Contains('-'))
    {
      string[] versesArray = verses.Split('-');
      int startVerse = int.Parse(versesArray[0]);
      int endVerse = int.Parse(versesArray[1]);

      reference = new Reference(book, chapter, startVerse, endVerse);
    }
    else
    {
      int verse = int.Parse(verses);
      reference = new Reference(book, chapter, verse);
    }

    return reference;
  }

  private static string[] GetDefaultVerse()
  {
    string reference = "Proverbs 3:5-6";
    string text = "" +
    "Trust in the Lord with all thine heart; and lean not unto thine own understanding." +
    "In all thy ways acknowledge him, and he shall direct thy paths.";

    string[] verseWithReference = { reference, text };
    return verseWithReference;
  }

  private static bool GetUserAnswer()
  {
    string userInput;
    bool inputValid;
    bool canContinue = false;

    do
    {
      Console.WriteLine("\nPress enter to continue or type 'quit' to finish: ");
      userInput = Console.ReadLine();

      if (userInput == "quit")
      {
        canContinue = false;
        break;
      }

      inputValid = string.IsNullOrWhiteSpace(userInput);

      if (inputValid)
      {
        canContinue = true;
      }
      else
      {
        Console.WriteLine(_incorrectInputMsg);
      }
    } while (!inputValid);

    return canContinue;
  }

  private static string GetConfigValue(string valueName)
  {
    string configFilename = "appsettings.json";

    try
    {
      IConfiguration config = new ConfigurationBuilder()
      .SetBasePath(System.IO.Directory.GetCurrentDirectory())
      .AddJsonFile(configFilename)
      .Build();

      return config[valueName];
    }
    catch (System.IO.FileNotFoundException)
    {
      Console.WriteLine($"Config file {configFilename} is not found.");
      return null;
    }
  }
}