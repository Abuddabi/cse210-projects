using System;

class ListingActivity : Activity
{
  private static readonly string _name = "Listing";
  private static readonly string _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
  private int _count;
  private List<string> _prompts;

  public ListingActivity(int duration = 0) : base(_name, _description, duration)
  {
    _prompts = new List<string>();
  }

  public void Run()
  {

  }

  public void GetRandomPrompt()
  {

  }

  public List<string> GetListFromUser()
  {
    return _prompts;
  }
}