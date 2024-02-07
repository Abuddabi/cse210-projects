using System;

class FileHandler
{
  private string _divider;
  private string _defaultSaveFile;
  private ConsoleHelper _console;
  private int _scoreFromLoad;

  public FileHandler()
  {
    _divider = "~|~";
    _defaultSaveFile = "goals.txt";
    _console = new ConsoleHelper();
    _scoreFromLoad = 0;
  }

  public string GetDivider()
  {
    return _divider;
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

  public void SaveGoals(int score, List<Goal> goals)
  {
    string fileName = GetFileName();

    using (StreamWriter outputFile = new StreamWriter(fileName))
    {
      outputFile.WriteLine(score);

      foreach (Goal goal in goals)
      {
        outputFile.WriteLine(goal.GetStringRepresentation());
      }
    }
  }

  public List<Goal> LoadGoals()
  {
    string fileName = GetFileName();
    List<Goal> goals = new List<Goal>();

    if (!File.Exists(fileName))
    {
      _console.RedMsg($"File \"{fileName}\" doesn't exist.");
      return goals;
    }

    string[] lines = File.ReadAllLines(fileName);

    // get score from the first line
    bool isValid = int.TryParse(lines.First(), out int score);
    if (!isValid)
    {
      score = 0;
    }
    _scoreFromLoad = score;

    // goals data from second line
    for (int i = 1, l = lines.Length; i < l; i++)
    {
      string[] parts = lines[i].Split(_divider);
      string className = parts[0];
      string name = parts[1];
      string description = parts[2];
      string points = parts[3];

      Goal goal = null;

      switch (className)
      {
        case "SimpleGoal":
          bool isComplete = bool.Parse(parts[4]);
          goal = new SimpleGoal(name, description, points, isComplete);
          break;
        case "EternalGoal":
          goal = new EternalGoal(name, description, points);
          break;
        case "ChecklistGoal":
          int bonus = int.Parse(parts[4]);
          int target = int.Parse(parts[5]);
          int completed = int.Parse(parts[6]);
          goal = new ChecklistGoal(name, description, points, target, bonus, completed);
          break;
      }

      if (goal != null)
      {
        goals.Add(goal);
      }
    }

    return goals;
  }

  public int GetScoreFromLoad()
  {
    return _scoreFromLoad;
  }
}