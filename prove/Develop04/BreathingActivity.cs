using System;

class BreathingActivity : Activity
{
  private static readonly string _name = "Breathing";
  private static readonly string _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
  public BreathingActivity(int duration = 0) : base(_name, _description, duration)
  {

  }

  public void Run()
  {

  }
}