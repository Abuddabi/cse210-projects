using System;

class Program
{
  static void Main(string[] args)
  {
    NetworkHelper.GetLocalIp();
    return;
    Console.WriteLine("Welcome to the Console Chat Application!");

    ConsoleHelper console = new ConsoleHelper();
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