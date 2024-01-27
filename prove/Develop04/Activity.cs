using System;

class Activity
{
  private string _name;
  private string _description;
  private int _duration;

  public Activity(string name, string description, int duration = 0)
  {
    _name = name;
    _description = description;
    _duration = duration;
  }

  public void SetDuration(int duration)
  {
    _duration = duration;
  }

  public string GetStartingMessage()
  {
    return "" +
    $"Welcome to the {_name} Activity.\n\n" +
    _description;
  }

  public void DisplayEndingMessage()
  {

  }

  public void ShowCountDown(int seconds)
  {

  }

  public string GetName()
  {
    return _name;
  }
}