class ModeratorUser : User
{
  public ModeratorUser(string username) : base(username)
  {

  }

  public ModeratorUser(string username, int type, bool isBlocked) : base(username, type, isBlocked)
  {

  }

  public override string GetTypeGreeting()
  {
    return " You're Moderator.";
  }

  public override void SetRoomsManager(RoomsManager roomsManager)
  {
    _roomsManager = roomsManager;
  }

  public override void CreateNewRoom(string roomName)
  {
    _roomsManager.CreateNewRoom(roomName);
  }

  public override void DeleteRoom(string roomName)
  {
    _roomsManager.DeleteRoom(roomName);
  }
}