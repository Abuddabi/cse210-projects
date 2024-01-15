using System;

class PromptGenerator
{
  static Random rnd = new Random();
  public readonly string[] _prompts = {
    "What accomplishment am I most proud of today?",
    "Describe a moment that made me laugh out loud.",
    "What challenge did I overcome today?",
    "How did I practice self-care today?",
    "What book, movie, or song had an impact on me today?",
    "Who inspired me today and why?",
    "In what ways did I learn or grow today?",
    "Describe a moment when I felt grateful.",
    "How did I handle a stressful situation today?",
    "What goal or task did I make progress on today?"
  };

  public string GetRandomPrompt()
  {
    int r = rnd.Next(_prompts.Length);
    string randomPrompt = _prompts[r];

    return randomPrompt;
  }
}