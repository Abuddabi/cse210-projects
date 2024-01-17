using System;
using System.Reflection;

class Menu
{
  /* 
    We should have methods with the same names as menu items. 
    Those methods should be public and static. 
   */
  static readonly string[] _menu = {
    "Write",
    "Display",
    "Load",
    "Save",
    "Quit"
  };

  static readonly PromptGenerator _promptGenerator = new PromptGenerator();

  static readonly FileHandler _fileHandler = new FileHandler();

  static readonly ColorConsole _console = new ColorConsole();

  public static string[] Generate()
  {
    string[] menu = _menu;

    // hide Save option from menu if there is nothing to save.
    if (!Journal._hasUnsaved)
    {
      menu = menu.Where(item => item != "Save").ToArray();
    }

    // hide Display option from menu if there is nothing to display.
    if (Journal.GetEntries().Count == 0)
    {
      menu = menu.Where(item => item != "Display").ToArray();
    }

    return menu;
  }

  public static void Print(string[] menu)
  {
    Console.WriteLine("Please select one of the following choices:");

    for (int i = 0; i < menu.Length; i++)
    {
      Console.WriteLine($"{i + 1}. {menu[i]}");
    }
  }

  public static void RunMethodByMenuItem(string menuItem)
  {
    MethodInfo method = typeof(Menu).GetMethod(menuItem);
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

  // MENU METHODS START
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

    Journal.AddEntry(newEntry);
    Journal._hasUnsaved = true;
  }

  public static void Display()
  {
    Journal.DisplayAll();
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


    List<string[]> entries = FileHandler.LoadFromFile(fileName);
    Journal.Clear();

    foreach (string[] entry in entries)
    {
      Entry newEntry = new Entry()
      {
        _date = entry[0],
        _promptText = entry[1],
        _entryText = entry[2]
      };

      Journal.AddEntry(newEntry);
    }

    _console.GreenMsg("Your Journal has been successfully loaded.\n");
  }

  public static void Save()
  {
    string fileName = AskForFile();

    FileHandler.SaveToFile(fileName);
    Journal._hasUnsaved = false;
  }

  public static void Quit()
  {
    if (Journal._hasUnsaved)
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
      }
    }

    Console.WriteLine("Bye bye!");
  }
  // MENU METHODS END

  static string AskForFile()
  {
    string fileName;
    bool isValid;
    string extensions = string.Join("|", _fileHandler._validFileExtensions);

    do
    {
      Console.WriteLine("What is the filename?");
      fileName = Console.ReadLine();
      isValid = _fileHandler.CheckExtension(fileName);

      if (!isValid)
      {
        _console.RedMsg($"Available file extensions: [{extensions}] Try one more time.");
      }
    } while (isValid != true);

    return fileName;
  }
}