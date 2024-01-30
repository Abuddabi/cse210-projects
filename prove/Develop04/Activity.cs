using System;

class Activity
{
  private readonly string _name;
  private readonly string _description;
  private int _duration;
  private readonly ConsoleHelper _console;

  public Activity(string name, string description, int duration = 0)
  {
    _name = name;
    _description = description;
    _duration = duration;

    _console = new ConsoleHelper();
  }

  public void Run()
  {
    DisplayStartingMessage();
    int seconds = _console.GetIntFromUser("How long in seconds, would you like your session? ", 3600); // 1 hour max
    _duration = seconds;

    Console.Clear();
    Console.WriteLine("Get ready...");
    ShowSpinner();

    RunActivityLogic();

    DisplayEndingMessage();
  }

  public virtual void RunActivityLogic()
  {
    /* 
      Will be overwritten in children classes 
    */
  }

  public void DisplayStartingMessage()
  {
    Console.Clear();
    Console.WriteLine($"Welcome to the {_name} Activity.\n\n{_description}\n");
  }

  public void DisplayEndingMessage()
  {
    Console.WriteLine("\n\nWell done!!");
    ShowSpinner();
    Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name} Activity.");
    ShowSpinner();
  }

  public void ShowSpinner(int seconds = 5)
  {
    char[] spinner = new char[]
    {
      '|',
      '/',
      '-',
      '\\',
      '|',
      '/',
      '-',
      '\\'
    };
    double animTimeStep = 0.5; // in seconds
    double remainSeconds = seconds;

    while (remainSeconds > 0)
    {
      foreach (char s in spinner)
      {
        Console.Write(s);
        Thread.Sleep((int)(animTimeStep * 1000));
        Console.Write("\b \b");

        remainSeconds -= animTimeStep;
        if (remainSeconds == 0)
        {
          break;
        }
      }
    }
  }

  protected void ShowCountDown(int seconds)
  {
    for (int i = seconds; i > 0; i--)
    {
      Console.Write(i);
      Thread.Sleep(1000);
      Console.Write("\b \b");
    }
  }

  public string GetName()
  {
    return _name;
  }

  protected int GetDuration()
  {
    return _duration;
  }
}