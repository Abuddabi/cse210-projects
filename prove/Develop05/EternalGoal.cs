using System;

class EternalGoal : Goal
{
  public EternalGoal(string name, string description, string points) : base(name, description, points)
  {

  }

  public override bool IsComplete()
  {
    // Eternal goal will never be completed.
    return false;
  }
}