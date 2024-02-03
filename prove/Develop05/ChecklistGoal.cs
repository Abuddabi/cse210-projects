using System;

class ChecklistGoal : Goal
{
  private int _amountCompleted;
  private int _target;
  private int _bonus;

  public ChecklistGoal(string name, string description, string points, int target, int bonus) : base(name, description, points)
  {
    _target = target;
    _bonus = bonus;
    _amountCompleted = 0;
  }

  public override void RecordEvent()
  {

  }

  public override bool IsComplete()
  {
    return false;
  }

  public override string GetStringRepresentation()
  {
    return "";
  }

  public override string GetDetailsString()
  {
    string details = base.GetDetailsString();
    details += $" -- Currently completed: {_amountCompleted}/{_target}";

    return details;
  }
}