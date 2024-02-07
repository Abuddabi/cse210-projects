using System;

class ChecklistGoal : Goal
{
  private int _amountCompleted;
  private int _target;
  private int _bonus;

  public ChecklistGoal(string name, string description, string points, int target, int bonus, int completed = 0) : base(name, description, points)
  {
    _target = target;
    _bonus = bonus;
    _amountCompleted = completed;
  }

  public override void RecordEvent()
  {
    _amountCompleted += 1;
    base.RecordEvent();
  }

  public override int GetPointsInt()
  {
    int points = base.GetPointsInt();
    if (IsComplete())
    {
      points += _bonus;
    }

    return points;
  }

  public override bool IsComplete()
  {
    return _amountCompleted == _target;
  }

  public override string GetStringRepresentation()
  {
    string representation = base.GetStringRepresentation();
    string divider = base.GetDivider();
    representation += $"{divider}{_bonus}{divider}{_target}{divider}{_amountCompleted}";
    return representation;
  }

  public override string GetDetailsString()
  {
    string details = base.GetDetailsString();
    details += $" -- Currently completed: {_amountCompleted}/{_target}";

    return details;
  }
}