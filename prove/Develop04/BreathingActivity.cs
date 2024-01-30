using System;

class BreathingActivity : Activity
{
  private static readonly string _name = "Breathing";
  private static readonly string _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";

  public BreathingActivity(int duration = 0) : base(_name, _description, duration)
  {

  }

  public override void RunActivityLogic()
  {
    string inText = "\n\nBreathe in...";
    string outText = "\nNow breathe out...";
    int count;
    bool isBreatheIn = true;
    int remainSeconds = base.GetDuration();

    Console.Write(inText);
    base.ShowCountDown(2);
    Console.Write(outText);
    base.ShowCountDown(3);
    remainSeconds -= 5;

    while (remainSeconds > 0)
    {
      if (isBreatheIn)
      {
        Console.Write(inText);
        count = 4;
      }
      else
      {
        Console.Write(outText);
        count = 6;
      }
      base.ShowCountDown(count);
      remainSeconds -= count;

      isBreatheIn = !isBreatheIn;
    }
  }
}