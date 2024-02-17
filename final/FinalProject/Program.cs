using System;
using System.Security.Authentication;

class Program
{
  static ConsoleHelper _console = new ConsoleHelper();
  static UsersManager _usersManager = new UsersManager();
  static RoomsManager _roomsManager = new RoomsManager();
  static User _currentUser;

  static void Main(string[] args)
  {
    Console.Clear();
    Console.WriteLine("\nWelcome to the Console Chat Application!");
    Start();
    RunMenu();
    Console.WriteLine("Chat ended. Goodbye!");
  }

  private static void Start()
  {
    _usersManager.LoadUsers();
    bool authFinished = false;
    while (!authFinished)
    {
      authFinished = Authentication();
    }
    _currentUser = _usersManager.GetCurrentUser();
    Console.Clear();
    _console.GreenMsg($"\nHello {_currentUser.GetUsername()}! Welcome to the Console Chat Application!");
    _roomsManager.LoadRooms();
    _roomsManager.LoadChatMessages();
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
    List<string> menu = new List<string>
    {
        "Show other Users",
        "Show available chat rooms",
        "Choose the chat room",
        "Exit"
    };
    if (_currentUser.GetUserType() > 1)
    {
      menu.InsertRange(0, new List<string> {
        "Add new room",
        "Delete room"
      });
    }
    int exitInt = menu.Count;

    bool exit = false;
    while (!exit)
    {
      Console.WriteLine("\nMENU: ");
      for (int i = 0; i < exitInt; i++)
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
    ChatRoom room = _roomsManager.FindRoomByName(roomName);
    if (room == null)
    {
      _console.RedMsg($"{roomName} room doesn't exist. Try another one.");
      return;
    }
    room.StartChat(_currentUser);
  }
}