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

    Console.Clear();
    Console.WriteLine("Menu Options:");
    for (int i = 1; i <= activities.Count; i++)
    {
      string activityName = activities[i - 1].GetName();
      Console.WriteLine($"  {i}. Start {activityName.ToLower()} activity");
    }
    int quitIndex = activities.Count + 1;
    Console.WriteLine($"  {quitIndex}. Quit");

    int userAnswer = GetUserAnswer(quitIndex);

    if (userAnswer < quitIndex)
    {
      // turn to index
      userAnswer--;
      Activity activity = activities[userAnswer];
    }
  }

  private static int GetUserAnswer(int maxValue)
  {
    int userNumber;
    bool inputValid;

    do
    {
      Console.Write("Select a choice from the menu: ");
      inputValid = int.TryParse(Console.ReadLine(), out userNumber);

      if (inputValid)
      {
        inputValid = userNumber > 0 && userNumber <= maxValue;
      }

      if (!inputValid)
      {
        _console.RedMsg("Your input is incorrect. Try one more time.");
      }
    } while (!inputValid);

    return userNumber;
  }
}