using System;

class ColorConsole
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
}