using System;

class ConsoleHelper
{
  private static string _invalidMsg;

  public ConsoleHelper()
  {
    _invalidMsg = "Your input is incorrect. Try one more time.";
  }

  private static void Write(string msg)
  {
    Console.WriteLine(msg);
    Console.ForegroundColor = ConsoleColor.White;
  }

  public void GreenMsg(string msg)
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Write(msg);
  }

  public void RedMsg(string msg)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Write(msg);
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
}