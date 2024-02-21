class RoomsManager
{
  private List<ChatRoom> _rooms;
  private FileHandler _fileHandler = new FileHandler();

  public RoomsManager()
  {
    _rooms = new List<ChatRoom>();
  }

  public RoomsManager(string[] rooms)
  {
    AddRooms(rooms);
  }

  public void AddRooms(string[] rooms)
  {
    foreach (string roomName in rooms)
    {
      AddRoom(roomName);
    }
  }

  public void AddRoom(string roomName)
  {
    ChatRoom room = new ChatRoom(roomName);
    _rooms.Add(room);
  }

  public void CreateNewRoom(string roomName)
  {
    AddRoom(roomName);
    _fileHandler.AppendToFile("rooms.txt", roomName);
  }

  public void DeleteRoom(string deleteRoom)
  {
    _rooms.RemoveAll(r => r.GetName() == deleteRoom);

    string fileName = "rooms.txt";
    string[] rooms = File.ReadAllLines(fileName);

    using (StreamWriter writer = new StreamWriter(fileName))
    {
      foreach (string roomName in rooms)
      {
        if (roomName == deleteRoom)
          continue;
        writer.WriteLine(roomName);
      }
    }

    fileName = "chats.txt";
    string delimiter = _fileHandler.GetDelimiter();
    string[] chatMsgs = File.ReadAllLines(fileName);
    string[] parts;
    using (StreamWriter writer = new StreamWriter(fileName))
    {
      foreach (string chatLine in chatMsgs)
      {
        parts = chatLine.Split(delimiter);
        if (parts[0] == deleteRoom)
          continue;
        writer.WriteLine(chatLine);
      }
    }
  }

  public void LoadRooms()
  {
    string fileName = "rooms.txt";
    string[] rooms = File.ReadAllLines(fileName);
    AddRooms(rooms);
  }

  public void PrintAllRooms()
  {
    foreach (ChatRoom room in _rooms)
    {
      Console.WriteLine(room.GetName());
    }
  }

  public ChatRoom FindRoomByName(string name)
  {
    foreach (ChatRoom room in _rooms)
    {
      if (room.GetName() == name)
        return room;
    }
    return null;
  }

  public void LoadChatMessages()
  {
    string fileName = "chats.txt";
    string delimiter = _fileHandler.GetDelimiter();

    string[] lines = File.ReadAllLines(fileName);

    string[] parts;
    string chatRoomName;
    string date;
    string username;
    string text;
    User user;
    ChatRoom room;
    Message msg;
    for (int i = 0, l = lines.Length; i < l; i++)
    {
      parts = lines[i].Split(delimiter);
      chatRoomName = parts[0];
      room = FindRoomByName(chatRoomName);
      if (room == null)
        continue;
      date = parts[1];
      username = parts[2];
      text = parts[3];

      user = new User(username);
      msg = new Message(user, text, date);
      room.AddMessage(msg);
    }
  }
}