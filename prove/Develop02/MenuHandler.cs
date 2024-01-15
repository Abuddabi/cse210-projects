using System;
using System.Reflection;

class MenuHandler
{
  // We should have methods with the same names as menu items. 
  // Those methods should be public and static.
  static readonly string[] _menu = {
    "Write",
    "Display",
    "Load",
    "Save",
    "Quit"
  };

  static readonly PromptGenerator _promptGenerator = new PromptGenerator();
  static readonly Journal _journal = new Journal();
  static readonly FileHandler _fileHandler = new FileHandler();
  static readonly ColorConsole _console = new ColorConsole();

  public void RunMenuLoop()
  {
    int userSelect;
    string chosenItem;
    string[] menu;

    do
    {
      menu = GenerateMenu();
      PrintMenu(menu);
      userSelect = GetUserAnswer(menu.Length);
      userSelect--; // turn to index
      chosenItem = menu[userSelect];
      RunMethodByMenuItem(chosenItem);
    } while (chosenItem != "Quit");
  }

  static void PrintMenu(string[] menu)
  {
    Console.WriteLine("Please select one of the following choices:");

    for (int i = 0; i < menu.Length; i++)
    {
      Console.WriteLine($"{i + 1}. {menu[i]}");
    }
  }

  static string[] GenerateMenu()
  {
    string[] menu;

    // remove Save option from menu if there is nothing to save.
    if (!_journal._hasUnsaved)
    {
      menu = _menu.Where(item => item != "Save").ToArray();
      return menu;
    }

    return _menu;
  }

  static int GetUserAnswer(int menuCount)
  {
    int userNumber;
    bool inputValid;

    do
    {
      Console.Write("What would you like to do? ");
      inputValid = int.TryParse(Console.ReadLine(), out userNumber);

      if (inputValid)
      {
        inputValid = userNumber > 0 && userNumber <= menuCount;
      }

      if (!inputValid)
      {
        _console.RedMsg("Your input is incorrect. Try one more time.");
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
      _console.RedMsg($"Method {menuItem} not found.");
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
    bool fileExists;
    string fileName;

    do
    {
      fileName = AskForFile();
      fileExists = File.Exists(fileName);

      if (!fileExists)
      {
        _console.RedMsg($"{fileName} file doesn't exist. Try another one.");
      }
    } while (!fileExists);


    _fileHandler.LoadFromFile(fileName);
  }

  public static void Save()
  {
    string fileName = AskForFile();

    _fileHandler.SaveToFile(fileName);
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
    string extensions = string.Join("|", _fileHandler._validFileExtensions);

    do
    {
      Console.WriteLine("What is the filename?");
      fileName = Console.ReadLine();
      isValid = _fileHandler.CheckFile(fileName);

      if (!isValid)
      {
        _console.RedMsg($"Available file extensions: [{extensions}] Try one more time.");
      }
    } while (isValid != true);

    return fileName;
  }
}