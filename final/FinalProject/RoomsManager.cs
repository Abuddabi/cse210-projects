class RoomsManager
{
  private List<ChatRoom> _rooms;

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
    ChatRoom room;
    foreach (string roomName in rooms)
    {
      room = new ChatRoom(roomName);
      _rooms.Add(room);
    }
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

    // TODO
  }
}