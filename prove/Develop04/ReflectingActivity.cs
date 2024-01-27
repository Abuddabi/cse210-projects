using System;

class ReflectingActivity : Activity
{
  private static readonly string _name = "Reflecting";
  private static readonly string _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
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