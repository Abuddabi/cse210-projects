using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

class Program
{
  private static Tuple<Reference, string> _theVerse;
  private static string _mode = "offline";

  private static string _incorrectInputMsg = "Your input is incorrect. Try one more time.";

  static async Task Main(string[] args)
  {
    string userInput;
    bool inputValid;

    Console.Clear();

    do
    {
      Console.Write("\nDo you want to use The English Standard Version (ESV) Bible online? [yes/no]: ");
      userInput = Console.ReadLine();
      inputValid = true;

      if (userInput == "yes")
      {
        _mode = "online";
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

    if (_mode == "online")
    {
      _theVerse = await GetVerseOnline();
    }
    else
    {
      _theVerse = GetDefaultVerse();
    }

    Scripture scripture = new Scripture(_theVerse.Item1, _theVerse.Item2);
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

  private static async Task<Tuple<Reference, string>> GetVerseOnline()
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

    Console.WriteLine("Loading... Please wait.");
    string result = await apiHandler.GetVerseByReference(reference);

    if (result == "Fail")
    {
      Console.WriteLine("We will continue with default verse. Press any key to continue.");
      Console.ReadKey();
      return GetDefaultVerse();
    }
    else
    {
      Reference referenceObj = GetReferenceFromString(reference);
      return Tuple.Create(referenceObj, result);
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

  private static Tuple<Reference, string> GetDefaultVerse()
  {
    Reference reference = new Reference("Proverbs", 3, 5, 6);
    string text = "" +
    "Trust in the Lord with all thine heart; and lean not unto thine own understanding." +
    "In all thy ways acknowledge him, and he shall direct thy paths.";

    return Tuple.Create(reference, text);
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
    try
    {
      IConfiguration config = new ConfigurationBuilder()
      .SetBasePath(System.IO.Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

      return config[valueName];
    }
    catch (System.IO.FileNotFoundException)
    {
      return null;
    }
  }
}