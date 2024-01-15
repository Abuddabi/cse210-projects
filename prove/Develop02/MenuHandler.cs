using System;
using System.Reflection;

class MenuHandler
{
  // We should have methods with the same names as menu items. 
  // Methods should be public and static.
  static List<string> _menu = new List<string>()
  {
    "Write",
    "Display",
    "Load",
    "Save",
    "Quit"
  };

  static readonly PromptGenerator _promptGenerator = new PromptGenerator();
  static readonly Journal _journal = new Journal();

  public void RunMenuLoop()
  {
    int userSelect;
    string chosenItem;

    do
    {
      PrintMenu();
      userSelect = GetUserAnswer();
      userSelect--; // turn to index
      List<string> menu = GenerateMenu();
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
    if (_journal._hasUnsaved == false)
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

    _journal.AddEntry(prompt, userText);
    _journal._hasUnsaved = true;
  }

  public static void Display()
  {
    Console.WriteLine("Display method");
  }

  public static void Load()
  {
    Console.WriteLine("Load method");
  }

  public static void Save()
  {
    Console.WriteLine("Save method");
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
}