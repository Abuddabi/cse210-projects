using System;

class ReflectingActivity : Activity
{
  private static readonly string _name = "Reflecting";
  private static readonly string _description = "Some description";
  private List<string> _prompts;
  private List<string> _questions;

  public ReflectingActivity(int duration = 0) : base(_name, _description, duration)
  {
    _prompts = new List<string>();
    _questions = new List<string>();
  }

  public void Run()
  {

  }

  public string GetRandomPrompt()
  {
    return "";
  }

  public string GetRandomQuestion()
  {
    return "";
  }

  public void DisplayPrompt()
  {

  }

  public void DisplayQuestions()
  {

  }
}