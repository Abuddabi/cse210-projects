using System;

class ListingActivity : Activity
{
  private static readonly string _name = "Listing";
  private static readonly string _description = "Some description";
  private int _count;
  private List<string> _prompts;

  public ListingActivity(int duration) : base(_name, _description, duration)
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