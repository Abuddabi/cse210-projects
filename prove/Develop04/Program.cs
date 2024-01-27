using System;

class Program
{
  private static ColorConsole _console = new ColorConsole();

  static void Main(string[] args)
  {
    List<Activity> activities = new List<Activity>()
    {
      new BreathingActivity(),
      new ReflectingActivity(),
      new ListingActivity()
    };

    PrintMenu(activities);

    int quitIndex = activities.Count + 1;
    int chosenItem = GetIntFromUser("Select a choice from the menu: ", quitIndex);

    if (chosenItem < quitIndex)
    {
      // turn to index
      chosenItem--;
      Activity activity = activities[chosenItem];
      RunActivity(activity);
    }
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

  private static int GetIntFromUser(string askMsg, int maxValue, int minValue = 1)
  {
    int userNumber;
    bool inputValid;

    do
    {
      Console.Write(askMsg);
      inputValid = int.TryParse(Console.ReadLine(), out userNumber);

      if (inputValid)
      {
        inputValid = userNumber >= minValue && userNumber <= maxValue;
      }

      if (!inputValid)
      {
        _console.RedMsg("Your input is incorrect. Try one more time.");
      }
    } while (!inputValid);

    return userNumber;
  }

  private static void RunActivity(Activity activity)
  {
    Console.Clear();
    Console.WriteLine(activity.GetStartingMessage() + "\n");
    int seconds = GetIntFromUser("How long in seconds, would you like your session? ", 3600); // 1 hour max
    activity.SetDuration(seconds);
  }
}