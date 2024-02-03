using System;

abstract class Goal
{
  private string _shortName;
  private string _description;
  private string _points;

  public Goal(string name, string description, string points)
  {
    _shortName = name;
    _description = description;
    _points = points;
  }

  public abstract void RecordEvent();

  public abstract bool IsComplete();

  public abstract string GetStringRepresentation();

  public virtual string GetDetailsString()
  {
    string x = IsComplete() ? "X" : " ";
    string details = $"[{x}] {_shortName} ({_description})";

    return details;
  }
}