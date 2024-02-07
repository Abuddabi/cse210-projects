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

  public void PrintGoodJob()
  {
    string[] phrase = new string[]
    {
      "    ______                             __           _____            __        __ ",
      "   /      \\                           /  |         /     |          /  |      /  |",
      "  /$$$$$$  |  ______    ______    ____$$ |         $$$$$ |  ______  $$ |____  $$ |",
      "  $$ | _$$/  /      \\  /      \\  /    $$ |            $$ | /      \\ $$      \\ $$ |",
      "  $$ |/    |/$$$$$$  |/$$$$$$  |/$$$$$$$ |       __   $$ |/$$$$$$  |$$$$$$$  |$$ |",
      "  $$ |$$$$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |      /  |  $$ |$$ |  $$ |$$ |  $$ |$$/ ",
      "  $$ \\__$$ |$$ \\__$$ |$$ \\__$$ |$$ \\__$$ |      $$ \\__$$ |$$ \\__$$ |$$ |__$$ | __ ",
      "  $$    $$/ $$    $$/ $$    $$/ $$    $$ |      $$    $$/ $$    $$/ $$    $$/ /  |",
      "   $$$$$$/   $$$$$$/   $$$$$$/   $$$$$$$/        $$$$$$/   $$$$$$/  $$$$$$$/  $$/ ",
      "                                                                                  "
    };

    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine("\n");
    Console.ForegroundColor = ConsoleColor.White;
    Thread.Sleep(400);
    for (int i = 0, l = phrase.Length; i < l; i++)
    {
      if (i == 5)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
      }
      else if (i == 7)
      {
        Console.ForegroundColor = ConsoleColor.Red;
      }
      Console.WriteLine(phrase[i]);
      Thread.Sleep(300);
    }
    Console.WriteLine("\n");
    Console.ResetColor();
    Thread.Sleep(1000);
  }
}