using System;

class Program
{
  static MenuHandler _menuHandler = new MenuHandler();
  static void Main(string[] args)
  {
    PrintWelcomeMsg();
    _menuHandler.RunMenuLoop();
  }

  static void PrintWelcomeMsg()
  {
    Console.WriteLine("\nWelcome to the Journal Program!");
  }
}