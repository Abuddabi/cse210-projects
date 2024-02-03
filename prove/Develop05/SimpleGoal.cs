using System;

class SimpleGoal : Goal
{
  private bool _isComplete;

  public SimpleGoal(string name, string description, string points, bool isComplete = false) : base(name, description, points)
  {
    _isComplete = isComplete;
  }

  public override void RecordEvent()
  {

  }

  public override bool IsComplete()
  {
    return _isComplete;
  }

  private void Completed()
  {
    _isComplete = true;
  }
  
  public override string GetStringRepresentation()
  {
    return "";
  }
}