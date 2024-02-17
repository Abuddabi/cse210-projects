class ChatRoom
{
  private List<Message> _messages;
  private ConsoleHelper _console = new ConsoleHelper();
  private string _name;
  private User _currentUser;
  private FileHandler _fileHandler = new FileHandler();

  public ChatRoom(string name)
  {
    _messages = new List<Message>();
    _name = name;
  }

  public void SendMessage(User sender, string content)
  {
    Message message = new Message(sender, content);
    _messages.Add(message);

    string delimiter = _fileHandler.GetDelimiter();
    string textForFile = $"{_name}{delimiter}{message.GetTextForFile()}";
    _fileHandler.AppendToFile("chats.txt", textForFile);
  }

  public void AddMessage(Message msg)
  {
    _messages.Add(msg);
  }

  public List<Message> GetMessages()
  {
    return _messages;
  }

  public string GetName()
  {
    return _name;
  }

  public void StartChat(User user)
  {
    _currentUser = user;

    Console.Clear();
    foreach (Message msg in _messages)
    {
      Console.WriteLine(msg.GetChatLine());
    }

    // Move the cursor to the bottom of the console window
    Console.SetCursorPosition(0, Console.WindowHeight - 3);
    // Write new content at the bottom of the console
    _console.GreenMsg($"Now you are in {_name} chat room.");
    Console.WriteLine("To exit the room type \"exit\"");
    _console.GreenMsg($"{_currentUser.GetUsername()}: ", false);
    string userMsg = Console.ReadLine();

    while (userMsg != "exit")
    {
      SendMessage(_currentUser, userMsg);
      UpdateChatDisplay();
      userMsg = Console.ReadLine();
    };
  }

  private void UpdateChatDisplay()
  {
    Console.Clear();
    foreach (Message msg in _messages)
    {
      Console.WriteLine(msg.GetChatLine());
    }
    Console.SetCursorPosition(0, Console.WindowHeight - 2);
    Console.WriteLine("To exit the room type \"exit\"");
    _console.GreenMsg($"{_currentUser.GetUsername()}: ", false);
  }
}