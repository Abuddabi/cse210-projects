using System;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Welcome to the Console Chat Application!");
    UsersManager usersManager = new UsersManager();
    Console.Write("\nPlease, write your username: ");
    string username = Console.ReadLine();

    Console.WriteLine("Here are all users of the chat: ");


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