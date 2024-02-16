using System;

class Program
{
  static void Main(string[] args)
  {
    NetworkHelper network = new NetworkHelper();
    ConsoleHelper console = new ConsoleHelper();

    Console.WriteLine("Welcome to the Console Chat Application!");
    Console.Write("\nListening for other users in local network: ");
    console.ShowCountdownAsync(5);

    // Discover services
    bool serviceDiscovered = network.DiscoverServices(5 * 1000);
    if (!serviceDiscovered)
    {
      Console.WriteLine("\nNo other users in local network. Announce service.");
      // Announce the service if no services were discovered
      network.AnnounceService("Chat_Service", 1234);
    }
    else
    {
      Console.WriteLine("Found Service!");
    }

    // Wait for user input to exit
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return;

    UsersManager usersManager = new UsersManager();
    bool authFinished;

    do
    {
      Console.WriteLine("\n"
      + "Do you want to: \n"
      + "1. Login\n"
      + "2. Signup");
      int userInput = console.GetIntFromUser("Please write your answer: ", 2);

      if (userInput == 1)
        authFinished = usersManager.Login();
      else if (userInput == 2)
        authFinished = usersManager.Signup();
      else
        authFinished = false;
    } while (!authFinished);

    Console.WriteLine("\nHere are all users of the chat: ");

    // TODO


    User user1 = new User("User1");
    User user2 = new User("User2");

    ChatRoom chatRoom = new ChatRoom();

    Console.WriteLine($"You are now chatting as {user1.GetUsername()}");
    Console.WriteLine("Type 'exit' to end the chat");

    string messageContent = "";

    while (messageContent != "exit")
    {
      Console.Write("You: ");
      // message from current user
      messageContent = Console.ReadLine();

      chatRoom.SendMessage(user1, user2, messageContent);

      Console.WriteLine("Waiting for response...");

      // Simulate response time
      System.Threading.Thread.Sleep(1000);

      List<Message> messages = chatRoom.GetMessages();
      foreach (Message message in messages)
      {
        Console.WriteLine($"{message.GetRecipient().GetUsername()}: {message.GetContent()}");
      }
    }

    Console.WriteLine("Chat ended. Goodbye!");
  }
}