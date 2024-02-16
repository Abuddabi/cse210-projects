using System;
using System.Security.Authentication;

class Program
{
  static ConsoleHelper _console = new ConsoleHelper();
  static UsersManager _usersManager = new UsersManager();
  static RoomsManager _roomsManager = new RoomsManager();

  static void Main(string[] args)
  {
    Console.WriteLine("Welcome to the Console Chat Application!");
    Start();
    RunMenu();
    Console.WriteLine("Chat ended. Goodbye!");
  }

  private static void Start()
  {
    bool authFinished = false;
    while (!authFinished)
    {
      authFinished = Authentication();
    }
    _usersManager.LoadUsers();
    string[] availableRooms = { "Main", "Nature", "Music", "Games", "Gospel" };
    _roomsManager.AddRooms(availableRooms);
  }

  private static bool Authentication()
  {
    Console.WriteLine("\n"
      + "Do you want to: \n"
      + "1. Login\n"
      + "2. Signup");
    int userInt = _console.GetIntFromUser("Please write your answer: ", 2);

    bool authFinished;
    if (userInt == 1)
      authFinished = _usersManager.Login();
    else if (userInt == 2)
      authFinished = _usersManager.Signup();
    else
      authFinished = false;

    return authFinished;
  }

  private static void RunMenu()
  {
    string[] menu = {
      "Show other Users",
      "Show available chat rooms",
      "Choose the chat room",
      "Exit",
    };
    int exitInt = menu.Length;

    bool exit = false;
    while (!exit)
    {
      Console.WriteLine("\nMENU: \n");
      for (int i = 0, l = exitInt; i < l; i++)
      {
        Console.WriteLine($"  {i + 1}. {menu[i]}");
      }

      int userChoice = _console.GetIntFromUser("Please choose menu option: ", exitInt);
      switch (userChoice)
      {
        case 1:
          ShowUsers();
          break;
        case 2:
          ShowRooms();
          break;
        case 3:
          ChooseRoom();
          break;
      }

      exit = userChoice == exitInt;
    }
  }

  private static void ShowUsers()
  {
    Console.WriteLine("\nHere are all other users of the chat: ");
    _usersManager.PrintUsers(true);
  }

  private static void ShowRooms()
  {
    Console.WriteLine("\nThere are several available rooms for chatting: ");
    _roomsManager.PrintAllRooms();
  }

  private static void ChooseRoom()
  {
    string roomName = _console.GetStringFromUser("Please write the name of the chat room you want to start speaking in: ", false);

  }
}