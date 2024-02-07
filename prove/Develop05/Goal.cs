using System;

abstract class Goal
{
  private string _shortName;
  private string _description;
  private string _points;
  private FileHandler _fHandler;

  public Goal(string name, string description, string points)
  {
    _shortName = name;
    _description = description;
    _points = points;
    _fHandler = new FileHandler();
  }

  public virtual void RecordEvent()
  {
    int points = GetPointsInt();
    Console.WriteLine($"Congratulations! You have earned {points} points!");
  }

  public abstract bool IsComplete();

  protected string GetDivider()
  {
    return _fHandler.GetDivider();
  }

  public virtual string GetStringRepresentation()
  {
    string divider = GetDivider();

    // for each child class className will be individual
    string className = GetType().Name;
    string representation = $"{className}{divider}{_shortName}{divider}{_description}{divider}{_points}";
    return representation;
  }

  public virtual string GetDetailsString()
  {
    string x = IsComplete() ? "X" : " ";
    string details = $"[{x}] {_shortName} ({_description})";

    return details;
  }

  public string GetName()
  {
    return _shortName;
  }

  public virtual int GetPointsInt()
  {
    int points = int.Parse(_points);
    return points;
  }
}