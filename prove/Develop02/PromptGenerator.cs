using System;

class PromptGenerator
{
  static Random rnd = new Random();
  public readonly List<string> _prompts = new List<string>()
  {
    "Who was the most interesting person I interacted with today? ",
    "What was the best part of my day? ",
    "How did I see the hand of the Lord in my life today? ",
    "What was the strongest emotion I felt today? ",
    "If I had one thing I could do over today, what would it be? "
  };

  public string GetRandomPrompt()
  {
    int r = rnd.Next(_prompts.Count);
    string randomPrompt = _prompts[r];

    return randomPrompt;
  }
}