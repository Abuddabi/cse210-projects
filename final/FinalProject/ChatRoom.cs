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

  public async Task StartChat(User currentUser, string apiKey = "")
  {
    _currentUser = currentUser;
    Robot robot = new Robot();

    Console.Clear();
    foreach (Message msg in _messages)
    {
      Console.WriteLine(msg.GetChatLine());
    }
    int footerStart = Console.WindowHeight - 3;
    if (_messages.Count < footerStart)
      Console.SetCursorPosition(0, footerStart);

    _console.GreenMsg($"Now you are in {_name} chat room.");
    Console.WriteLine("To exit the room type \"exit\"");
    _console.GreenMsg($"{_currentUser.GetUsername()}: ", false);
    string userMsg = Console.ReadLine();

    while (userMsg != "exit")
    {
      SendMessage(_currentUser, userMsg);

      if (apiKey != "")
      {
        UpdateChatDisplay(true);
        string robotAnswer = await robot.GetAnswer(userMsg, apiKey);
        if (robotAnswer != "")
          SendMessage(robot, robotAnswer);
      }
      UpdateChatDisplay();
      userMsg = Console.ReadLine();
    };
  }

  private void UpdateChatDisplay(bool waitForRobot = false)
  {
    Console.Clear();
    foreach (Message msg in _messages)
    {
      Console.WriteLine(msg.GetChatLine());
    }
    int messagesHeight = _messages.Count;

    if (waitForRobot)
    {
      _console.RedMsg("Waiting for Robot answer...");
      messagesHeight++;
    }

    int footerStart = Console.WindowHeight - 2;
    if (messagesHeight < footerStart)
      Console.SetCursorPosition(0, footerStart);

    Console.WriteLine("To exit the room type \"exit\"");
    _console.GreenMsg($"{_currentUser.GetUsername()}: ", false);
  }
}