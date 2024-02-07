/*
  I added FileHandler file and I added ASCII art 
  and print it when user record an event.
  Minimum console/terminal width for ASCII art - 82!
  Please, just use your console/terminal in full screen mode.
 */

using System;

class Program
{
  private static ConsoleHelper _console = new ConsoleHelper();
  private static string[] _menu = {
    "Create New Goal",
    "List Goals",
    "Save Goals",
    "Load Goals",
    "Record Event",
    "Quit"
  };

  static void Main(string[] args)
  {
    GoalManager goalManager = new GoalManager();
    int userChoice;

    do
    {
      goalManager.DisplayPlayerInfo();
      PrintMenu();
      userChoice = _console.GetIntFromUser("Select a choice from the menu: ", _menu.Length);
      userChoice--;

      switch (userChoice) // userChoice == index in _menu array
      {
        case 0:
          goalManager.CreateGoal();
          break;
        case 1:
          goalManager.ListGoalDetails();
          break;
        case 2:
          goalManager.SaveGoals();
          break;
        case 3:
          goalManager.LoadGoals();
          break;
        case 4:
          goalManager.RecordEvent();
          break;
      }
    } while (userChoice != _menu.Length - 1);
  }

  private static void PrintMenu()
  {
    Console.WriteLine("Menu Options:");
    int length = _menu.Length;

    for (int i = 1; i <= length; i++)
    {
      Console.WriteLine($"  {i}. {_menu[i - 1]}");
    }
  }
}