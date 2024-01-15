using System;
using System.Reflection;

class MenuHandler
{
  // We should have methods with the same names as menu items. 
  // Those methods should be public and static.
  static readonly List<string> _menu = new List<string>()
  {
    "Write",
    "Display",
    "Load",
    "Save",
    "Quit"
  };

  static readonly PromptGenerator _promptGenerator = new PromptGenerator();
  static readonly Journal _journal = new Journal();

  static readonly string[] _validFileExtensions = {
    ".txt",
    ".md"
  };

  public void RunMenuLoop()
  {
    int userSelect;
    string chosenItem;
    List<string> menu;

    do
    {
      PrintMenu();
      userSelect = GetUserAnswer();
      userSelect--; // turn to index
      menu = GenerateMenu();
      chosenItem = menu[userSelect];
      RunMethodByMenuItem(chosenItem);
    } while (chosenItem != "Quit");
  }

  static void PrintMenu()
  {
    List<string> menu = GenerateMenu();

    Console.WriteLine("Please select one of the following choices:");

    for (int i = 0; i < menu.Count; i++)
    {
      Console.WriteLine($"{i + 1}. {menu[i]}");
    }
  }

  static List<string> GenerateMenu()
  {
    List<string> menu;

    // remove Save option from menu if there is nothing to save.
    if (!_journal._hasUnsaved)
    {
      menu = _menu.Where(item => item != "Save").ToList();
    }
    else
    {
      menu = _menu;
    }

    return menu;
  }

  static int GetUserAnswer()
  {
    int userNumber;
    bool inputValid;

    do
    {
      Console.Write("What would you like to do? ");
      inputValid = int.TryParse(Console.ReadLine(), out userNumber);

      if (inputValid)
      {
        inputValid = userNumber > 0 && userNumber <= _menu.Count;
      }

      if (!inputValid)
      {
        Console.WriteLine("Your input is incorrect. Try one more time.");
      }
    } while (!inputValid);

    return userNumber;
  }

  static void RunMethodByMenuItem(string menuItem)
  {
    MethodInfo method = typeof(MenuHandler).GetMethod(menuItem);
    bool hasMethod = method != null;

    if (hasMethod)
    {
      method.Invoke(null, null);
    }
    else
    {
      Console.WriteLine($"Method {menuItem} not found.");
    }
  }


  public static void Write()
  {
    string prompt = _promptGenerator.GetRandomPrompt();
    Console.WriteLine($"{prompt}");
    string userText = Console.ReadLine();

    Entry newEntry = new Entry()
    {
      _date = DateTime.Now.ToShortDateString(),
      _promptText = prompt,
      _entryText = userText
    };

    _journal.AddEntry(newEntry);
    _journal._hasUnsaved = true;
  }

  public static void Display()
  {
    _journal.DisplayAll();
  }

  public static void Load()
  {
    string fileName = AskForFile();
    _journal.LoadFromFile(fileName);
  }

  public static void Save()
  {
    string fileName = AskForFile();

    _journal.SaveToFile(fileName);
    _journal._hasUnsaved = false;
  }

  public static void Quit()
  {
    if (_journal._hasUnsaved)
    {
      string userText;

      do
      {
        Console.Write("You have unsaved records. Do you want to save them before quitting? [yes/no]: ");
        userText = Console.ReadLine();
      } while (userText != "yes" && userText != "no");

      if (userText == "yes")
      {
        Save();
        _journal._hasUnsaved = false;
      }
    }

    Console.WriteLine("Bye bye!");
  }

  static string AskForFile()
  {
    string fileName;
    bool isValid;

    do
    {
      Console.WriteLine("What is the filename?");
      fileName = Console.ReadLine();
      isValid = false;

      foreach (string ext in _validFileExtensions)
      {
        if (fileName.Contains(ext))
        {
          isValid = true;
          break;
        }
      }

      if (!isValid)
      {
        Console.WriteLine($"Available file extensions: [{string.Join("|", _validFileExtensions)}] Try one more time.");
      }
    } while (isValid != true);

    return fileName;
  }
}