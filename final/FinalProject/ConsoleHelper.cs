using System;

class ConsoleHelper
{
  private static string _invalidMsg;

  public ConsoleHelper()
  {
    _invalidMsg = "Your input is incorrect. Try one more time.";
  }

  private static void Write(string msg, bool line)
  {
    if (line)
      Console.WriteLine(msg);
    else
      Console.Write(msg);
    Console.ResetColor();
    // Console.ForegroundColor = ConsoleColor.White;
  }

  public void GreenMsg(string msg, bool line = true)
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Write(msg, line);
  }

  public void RedMsg(string msg, bool line = true)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Write(msg, line);
  }

  public int GetIntFromUser(string askMsg, int maxValue = int.MaxValue, int minValue = 1)
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
      inputValid = CheckValid(inputValid);
    } while (!inputValid);

    return userNumber;
  }

  public string GetStringFromUser(string askMsg, bool onTheSameLine = true)
  {
    string userString;
    bool inputValid;

    do
    {
      if (onTheSameLine)
      {
        Console.Write(askMsg);
      }
      else
      {
        Console.WriteLine(askMsg);
      }

      userString = Console.ReadLine();
      inputValid = CheckValid(userString.Length > 0);
    } while (!inputValid);

    return userString;
  }

  private bool CheckValid(bool condition)
  {
    if (!condition)
    {
      RedMsg(_invalidMsg);
    }

    return condition;
  }

  public void ShowCountDown(int seconds)
  {
    for (int i = seconds; i > 0; i--)
    {
      Console.Write(i);
      Thread.Sleep(1000);
      Console.Write("\b \b");
    }
  }

  public async Task ShowCountdownAsync(int seconds)
  {
    for (int i = seconds; i > 0; i--)
    {
      Console.Write(i);
      await Task.Delay(1000);
      Console.Write("\b \b");
    }
  }

  public void KeyToContinue()
  {
    Console.Write("Please, press any key to continue: ");
    Console.ReadKey();
    Console.Clear();
  }
}