using System;

class BreathingActivity : Activity
{
  private static readonly string _name = "Breathing";
  private static readonly string _description = "Some description";
  public BreathingActivity(int duration = 0) : base(_name, _description, duration)
  {

  }

  public void Run()
  {

  }
}