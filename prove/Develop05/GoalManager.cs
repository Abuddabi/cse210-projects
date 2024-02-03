using System;
using System.IO;

class GoalManager
{
  private static ConsoleHelper _console;
  private List<Goal> _goals;
  private int _score;
  private string _defaultSaveFile;

  public GoalManager()
  {
    _console = new ConsoleHelper();
    _goals = new List<Goal>();
    _score = 0;
    _defaultSaveFile = "goals.txt";
  }

  public void Start()
  {

  }

  public void DisplayPlayerInfo()
  {
    Console.WriteLine($"\nYou have {_score} points.\n");
  }

  public void ListGoalNames()
  {

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

  }

  public void SaveGoals()
  {
    string fileName = GetFileName();

    using (StreamWriter outputFile = new StreamWriter(fileName))
    {
      outputFile.WriteLine(_score);

      foreach (Goal goal in _goals)
      {
        outputFile.WriteLine(goal.GetStringRepresentation());
      }
    }
  }

  public void LoadGoals()
  {
    string fileName = GetFileName();
    string[] lines = File.ReadAllLines(fileName);

    // get score from the first line
    bool isValid = int.TryParse(lines.First(), out int score);
    if (!isValid)
    {
      score = 0;
    }
    _score = score;

    // goals data from second line
    for (int i = 1, l = lines.Length; i < l; i++)
    {
      string[] parts = lines[i].Split(":");
      string className = parts[0];
      parts = parts[1].Split(",");
      string name = parts[0];
      string description = parts[1];
      string points = parts[2];

      Goal goal = null;

      switch (className)
      {
        case "SimpleGoal":
          bool isComplete = bool.Parse(parts[3]);
          goal = new SimpleGoal(name, description, points, isComplete);
          break;
        case "EternalGoal":
          goal = new EternalGoal(name, description, points);
          break;
        case "ChecklistGoal":
          int bonus = int.Parse(parts[3]);
          int target = int.Parse(parts[4]);
          int completed = int.Parse(parts[5]);
          goal = new ChecklistGoal(name, description, points, target, bonus, completed);
          break;
      }

      if (goal != null)
      {
        _goals.Add(goal);
      }
    }
  }

  private string GetFileName()
  {
    Console.Write($"What is the filename for the goal file? (press Enter for default \"{_defaultSaveFile}\") ");
    string fileName = Console.ReadLine();

    if (fileName.Length == 0)
    {
      fileName = _defaultSaveFile;
    }

    return fileName;
  }
}