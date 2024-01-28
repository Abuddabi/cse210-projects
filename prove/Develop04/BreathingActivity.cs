using System;

class BreathingActivity : Activity
{
  private static readonly string _name = "Breathing";
  private static readonly string _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
  private bool _in = true;

  public BreathingActivity(int duration = 0) : base(_name, _description, duration)
  {

  }

  public override void Run()
  {
    string inText = "\n\nBreathe in...";
    string outText = "\nNow breathe out...";
    int remainSeconds = GetDuration();

    Console.Write(inText);
    ShowCountDown(2); // parent method
    Console.Write(outText);
    ShowCountDown(3);
    remainSeconds -= 5;

    while (remainSeconds > 0)
    {
      if (_in)
      {
        Console.Write(inText);
        ShowCountDown(4);
        remainSeconds -= 4;
      }
      else
      {
        Console.Write(outText);
        ShowCountDown(6);
        remainSeconds -= 6;
      }

      _in = !_in;
    }
  }
}