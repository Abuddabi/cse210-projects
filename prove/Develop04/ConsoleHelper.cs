using System;

class ConsoleHelper
{
  static void Write(string msg)
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

  public int GetIntFromUser(string askMsg, int maxValue, int minValue = 1)
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
        RedMsg("Your input is incorrect. Try one more time.");
      }
    } while (!inputValid);

    return userNumber;
  }
}