using System;
using System.IO;
using Microsoft.Extensions.Configuration;

class Program
{
  private static ConsoleHelper _console = new ConsoleHelper();
  private static UsersManager _usersManager = new UsersManager();
  private static RoomsManager _roomsManager = new RoomsManager();
  private static User _currentUser;

  static async Task Main(string[] args)
  {
    Console.Clear();
    Console.WriteLine("\nWelcome to the Console Chat Application!");
    Start();
    await RunMenu();
    Console.WriteLine("\nChat ended. Goodbye!");
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
    _console.GreenMsg($"\nHello {_currentUser.GetUsername()}!{_currentUser.GetTypeGreeting()} Welcome to the Console Chat Application!");
    _roomsManager.LoadRooms();
    _roomsManager.LoadChatMessages();

    _currentUser.SetRoomsManager(_roomsManager);
    _currentUser.SetUsersManager(_usersManager);
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

  private static async Task RunMenu()
  {
    List<string> menu;
    int exitInt = 0;
    bool exit = false;
    while (!exit)
    {
      menu = GetMenu();
      Console.WriteLine("\nMENU: ");
      for (int i = 0, j = 0, l = menu.Count; i < l; i++)
      {
        if (menu[i] == "=")
          Console.WriteLine("  ====================");
        else
          Console.WriteLine($"  {++j}. {menu[i]}");
        exitInt = j;
      }

      int userChoice = _console.GetIntFromUser("Please choose menu option: ", exitInt);
      exit = userChoice == exitInt;
      if (exit)
        return;

      await RunMethod(userChoice);
    }
  }

  private static List<string> GetMenu()
  {
    // "=" - doesn't have index number in shown menu
    List<string> menu = new List<string>
    {
        "Show other Users",           // 1
        "Show available chat rooms",  // 2
        "Choose the chat room"        // 3
    };
    int userType = _currentUser.GetUserType();
    if (userType > 1) // For Moderators
    {
      menu.AddRange(new List<string> {
        "=",
        "Add new room", // 4
        "Delete room"   // 5
      });
    }
    if (userType > 2) // For Admins
    {
      menu.AddRange(new List<string> {
        "=",
        "Block user",       // 6
        "Unblock user",     // 7
        "Change user type"  // 8
      });
    }
    menu.Add("=");
    menu.Add("Exit");

    return menu;
  }

  private static async Task RunMethod(int userChoice)
  {
    // we can change it in future
    switch (userChoice)
    {
      case 1:
        ShowUsers();
        break;
      case 2:
        ShowRooms();
        break;
      case 3:
        await ChooseRoom();
        break;
      case 4:
        CreateRoom();
        break;
      case 5:
        DeleteRoom();
        break;
      case 6:
        BlockUser();
        break;
      case 7:
        UnblockUser();
        break;
      case 8:
        ChangeUserType();
        break;
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

  private static async Task ChooseRoom()
  {
    string roomName = _console.GetStringFromUser("Please write the name of the chat room you want to start speaking in: ", false);
    ChatRoom room = _roomsManager.FindRoomByName(roomName);
    if (room == null)
    {
      _console.RedMsg($"{roomName} room doesn't exist. Try another one.");
      return;
    }

    string apiKey = "";
    string userAnswer = _console.GetStringFromUser("Do you want to use API robot to answer in the chat? (yes/no): ");
    if (userAnswer == "yes")
    {
      apiKey = GetConfigValue("API_KEYS:ROBOT_CHAT");
      if (apiKey == null)
      {
        apiKey = _console.GetStringFromUser("Please, enter API KEY (or type \"cancel\"): ");
        if (apiKey == "cancel")
          apiKey = "";
      }
    }
    await room.StartChat(_currentUser, apiKey);
  }

  private static void CreateRoom()
  {
    string roomName = _console.GetStringFromUser("Please write the name of the new room: ", false);
    ChatRoom exist = _roomsManager.FindRoomByName(roomName);
    if (exist != null)
    {
      _console.RedMsg($"Room {roomName} is already exist.");
      return;
    }

    _currentUser.CreateNewRoom(roomName);
    _console.GreenMsg($"\nRoom {roomName} is created.");
    _console.KeyToContinue();
  }

  private static void DeleteRoom()
  {
    string roomName = _console.GetStringFromUser("Please write the name of the room which you want to delete: ", false);
    ChatRoom exist = _roomsManager.FindRoomByName(roomName);
    if (exist == null)
    {
      _console.RedMsg($"Room {roomName} doesn't exist.");
      return;
    }

    _console.RedMsg("With the room you will delete all messages in that room.");
    Console.Write("Are you sure? (yes/no): ");
    string confident = Console.ReadLine();
    if (confident == "no")
      return;

    _currentUser.DeleteRoom(roomName);
    _console.GreenMsg($"\nRoom {roomName} is deleted.");
    _console.KeyToContinue();
  }

  private static void BlockUser()
  {
    ChangeBlockStatus(1);
  }

  private static void UnblockUser()
  {
    ChangeBlockStatus(2);
  }

  private static void ChangeBlockStatus(int typeInt) // type == 1 - block, 2 - unblock
  {
    string type = "";
    if (typeInt == 1)
      type = "block";
    else if (typeInt == 2)
      type = "unblock";

    string username = _console.GetStringFromUser($"Please, write the username of the user which you want to {type}: ", false);
    if (!_usersManager.IsUserExists(username))
    {
      _console.RedMsg($"User {username} doesn't exist. Try one more time.");
      return;
    }
    else if (typeInt == 1 && username == _currentUser.GetUsername())
    {
      _console.RedMsg("You can't block yourself.");
      return;
    }
    if (typeInt == 1)
      _currentUser.BlockUser(username);
    else if (typeInt == 2)
      _currentUser.UnblockUser(username);

    _console.GreenMsg($"User {username} is successfully {type}ed!");
    _console.KeyToContinue();
  }

  private static void ChangeUserType()
  {
    string username = _console.GetStringFromUser($"Please, enter the username of the user whose type you want to change: ", false);
    if (!_usersManager.IsUserExists(username))
    {
      _console.RedMsg($"User {username} doesn't exist. Try one more time.");
      return;
    }
    int type = _console.GetIntFromUser("Desired type (3 - Admin, 2 - Moderator, 1 - Regular user): ", 3, 1);
    _currentUser.ChangeUserType(username, type);
    string typeStr = type == 3 ? "an Admin" : type == 2 ? "a Moderator" : "a regular user";
    _console.GreenMsg($"The type is successfully changed. Now {username} is {typeStr}.");
    _console.KeyToContinue();
  }

  private static string GetConfigValue(string valueName)
  {
    string configFilename = "appsettings.json";

    try
    {
      IConfiguration config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile(configFilename)
      .Build();

      return config[valueName];
    }
    catch (FileNotFoundException)
    {
      _console.RedMsg($"Config file {configFilename} is not found.");
      return null;
    }
  }
}