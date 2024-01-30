using System;

class Program
{
  private static readonly ConsoleHelper _console = new ConsoleHelper();

  static void Main(string[] args)
  {
    List<Activity> activities = new List<Activity>()
    {
      new BreathingActivity(),
      new ReflectingActivity(),
      new ListingActivity()
    };
    int quitOption = activities.Count + 1;
    int chosenItem;

    do
    {
      PrintMenu(activities);
      chosenItem = _console.GetIntFromUser("Select a choice from the menu: ", quitOption);

      if (chosenItem < quitOption)
      {
        // turn to index
        chosenItem--;
        Activity activity = activities[chosenItem];
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