using System;
using System.IO;

class GoalManager
{
  private static ConsoleHelper _console;
  private List<Goal> _goals;
  private int _score;
  private FileHandler _fHandler;

  public GoalManager()
  {
    _console = new ConsoleHelper();
    _goals = new List<Goal>();
    _score = 0;
    _fHandler = new FileHandler();
  }

  public void DisplayPlayerInfo()
  {
    Console.WriteLine($"\nYou have {_score} points.\n");
  }

  public void ListGoalNames(List<Goal> goals)
  {
    Console.WriteLine("The goals are:");
    int length = goals.Count;

    for (int i = 0; i < length; i++)
    {
      string goalString = goals[i].GetName();
      Console.WriteLine($"{i + 1}. {goalString}");
    }
  }

  public void ListGoalDetails()
  {
    int length = _goals.Count;

    if (length > 0)
    {
      Console.WriteLine("The goals are:");

      for (int i = 0; i < length; i++)
      {
        string goalString = _goals[i].GetDetailsString();
        Console.WriteLine($"{i + 1}. {goalString}");
      }
    }
    else
    {
      Console.WriteLine("You don't have goals yet. Try create a new one!");
    }
  }

  public void CreateGoal()
  {
    string[] goalsTypes = {
      "Simple",
      "Eternal",
      "Checklist"
    };
    int length = goalsTypes.Length;

    Console.WriteLine("The types of goals are: ");
    for (int i = 1; i <= length; i++)
    {
      Console.WriteLine($"  {i}. {goalsTypes[i - 1]} Goal");
    }

    int userChoice = _console.GetIntFromUser("Which type of goal would you like to create? ", length);
    userChoice--;

    string name = _console.GetStringFromUser("What is the name of your goal? ");
    string description = _console.GetStringFromUser("What is a short description of it? ");
    string points = _console.GetStringFromUser("What is the amount of points associated with this goal? ");

    Goal goal = null;

    switch (userChoice) // userChoice == index in goalsTypes array 
    {
      case 0:
        goal = new SimpleGoal(name, description, points);
        break;
      case 1:
        goal = new EternalGoal(name, description, points);
        break;
      case 2:
        int target = _console.GetIntFromUser("How many times does this goal need to be accomplished for a bonus? ");
        int bonus = _console.GetIntFromUser($"What is the bonus for accomplishing it {target} time{(target != 1 ? "s" : "")}? ");
        goal = new ChecklistGoal(name, description, points, target, bonus);
        break;
    }

    if (goal != null)
    {
      _goals.Add(goal);
    }
  }

  public void RecordEvent()
  {
    List<Goal> uncompletedGoals = _goals.FindAll(goal => !goal.IsComplete());
    if (uncompletedGoals.Count == 0)
    {
      Console.WriteLine("You don't have uncompleted goals.");
      return;
    }
    ListGoalNames(uncompletedGoals);
    int accomplished = _console.GetIntFromUser("Which goal did you accomplish? ", uncompletedGoals.Count);
    accomplished--; // turn to index

    Goal goal = uncompletedGoals[accomplished];
    goal.RecordEvent();
    _score += goal.GetPointsInt();
    Console.WriteLine($"You now have {_score} points.");
    _console.PrintGoodJob();
  }

  public void SaveGoals()
  {
    _fHandler.SaveGoals(_score, _goals);
  }

  public void LoadGoals()
  {
    _goals = _fHandler.LoadGoals();
    _score = _fHandler.GetScoreFromLoad();
  }
}