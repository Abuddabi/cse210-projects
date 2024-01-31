using System;

class ListingActivity : Activity
{
  private static readonly string _name = "Listing";
  private static readonly string _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
  private int _count;
  private readonly List<string> _prompts;

  public ListingActivity(int duration = 0) : base(_name, _description, duration)
  {
    _prompts = new List<string>()
    {
      "Who are people that you appreciate?",
      "What are personal strengths of yours?",
      "Who are people that you have helped this week?",
      "When have you felt the Holy Ghost this month?",
      "Who are some of your personal heroes?"
    };
  }

  public override void RunActivityLogic()
  {
    int remainSeconds = base.GetDuration();
    Console.WriteLine("\nList as many responses you can to the following prompt:");
    DisplayPrompt();
    Console.Write("You may begin in: ");
    base.ShowCountDown(5);
    remainSeconds -= 5;

    Console.WriteLine(); // for the empty output line
    List<string> userList = GetListFromUser(remainSeconds);
    _count = userList.Count;
    Console.Write($"You listed {_count} items!");
  }

  private void DisplayPrompt()
  {
    string prompt = base.GetRandomPrompt(_prompts);
    Console.WriteLine($" --- {prompt} --- ");
  }

  private List<string> GetListFromUser(int remainSeconds)
  {
    string userAnswer;
    List<string> list = new List<string>();
    DateTime endTime = DateTime.Now.AddSeconds(remainSeconds);

    while (DateTime.Now < endTime)
    {
      Console.Write("> ");
      userAnswer = Console.ReadLine();
      list.Add(userAnswer);
    }

    return list;
  }
}