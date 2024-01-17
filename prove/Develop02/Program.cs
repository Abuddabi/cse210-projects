using System;

class Program
{
  static readonly ColorConsole _console = new ColorConsole();

  static void Main(string[] args)
  {
    PrintWelcomeMsg();
    RunMenuLoop();
  }

  static void PrintWelcomeMsg()
  {
    Console.WriteLine("\nWelcome to the Journal Program!");
  }

  static void RunMenuLoop()
  {
    string[] menu;
    int userSelect;
    string chosenItem;

    do
    {
      menu = Menu.Generate();
      Menu.Print(menu);
      userSelect = GetUserAnswer(menu.Length);
      userSelect--; // turn to index
      chosenItem = menu[userSelect];
      Menu.RunMethodByMenuItem(chosenItem);
    } while (chosenItem != "Quit");
  }

  static int GetUserAnswer(int maxValue)
  {
    int userNumber;
    bool inputValid;

    do
    {
      Console.Write("What would you like to do? ");
      inputValid = int.TryParse(Console.ReadLine(), out userNumber);

      if (inputValid)
      {
        inputValid = userNumber > 0 && userNumber <= maxValue;
      }

      if (!inputValid)
      {
        _console.RedMsg("Your input is incorrect. Try one more time.");
      }
    } while (!inputValid);

    return userNumber;
  }

}