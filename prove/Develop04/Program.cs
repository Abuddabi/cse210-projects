/* 
  I made sure no random questions are selected until they have all been used at least once in that session.
  Also I created separate ConsoleHelper class, which helps to get integer from the user and to show color messages.
 */

using System;

class Program
{
  private static readonly ConsoleHelper _console = new ConsoleHelper();
  private static List<Activity> _activities;

  static void Main(string[] args)
  {
    _activities = new List<Activity>()
    {
      new BreathingActivity(),
      new ReflectingActivity(),
      new ListingActivity()
    };
    int quitOption = _activities.Count + 1;
    int chosenItem;

    do
    {
      PrintMenu(_activities);
      chosenItem = _console.GetIntFromUser("Select a choice from the menu: ", quitOption);

      if (chosenItem < quitOption)
      {
        // turn to index
        chosenItem--;
        Activity activity = _activities[chosenItem];
        activity.Run();
      }
    } while (chosenItem != quitOption);
  }

  private static void PrintMenu(List<Activity> activities)
  {
    Console.Clear();
    Console.WriteLine("Menu Options:");
    for (int i = 1; i <= activities.Count; i++)
    {
      string activityName = activities[i - 1].GetName();
      Console.WriteLine($"  {i}. Start {activityName.ToLower()} activity");
    }
    Console.WriteLine($"  {activities.Count + 1}. Quit");
  }
}