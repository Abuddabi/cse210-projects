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

  }

  private void OneMoreGoal()
  {
    _amountCompleted += 1;
  }

  public override bool IsComplete()
  {
    return _amountCompleted == _target;
  }

  public override string GetStringRepresentation()
  {
    string representation = base.GetStringRepresentation();
    representation += $",{_bonus},{_target},{_amountCompleted}";
    return representation;
  }

  public override string GetDetailsString()
  {
    string details = base.GetDetailsString();
    details += $" -- Currently completed: {_amountCompleted}/{_target}";

    return details;
  }
}