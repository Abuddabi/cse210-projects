class User
{
  private string _username;
  private int _type; // 1 - regular user, 2 - moderator, 3 - admin
  private bool _isBlocked;
  protected RoomsManager _roomsManager;
  protected UsersManager _usersManager;

  public User(string username) // new User
  {
    _username = username;
    _type = 1;
    _isBlocked = false;
  }

  public User(string username, int type, bool isBlocked) // loaded user
  {
    _username = username;
    _type = type;
    _isBlocked = isBlocked;
  }

  public string GetUsername()
  {
    return _username;
  }

  public void ChangeType(int type)
  {
    _type = type;
  }

  public int GetUserType()
  {
    return _type;
  }

  public void Block()
  {
    _isBlocked = true;
  }

  public void Unblock()
  {
    _isBlocked = false;
  }

  public virtual bool IsBlocked()
  {
    return _isBlocked;
  }

  public virtual string GetTypeGreeting()
  {
    return "";
  }

  public virtual void SetRoomsManager(RoomsManager roomsManager)
  {
    return; // Forbidden for regular users.
  }

  public virtual void SetUsersManager(UsersManager usersManager)
  {
    return; // Forbidden for regular users.
  }

  public virtual void CreateNewRoom(string roomName)
  {
    return; // Forbidden for regular users.
  }

  public virtual void DeleteRoom(string roomName)
  {
    return; // Forbidden for regular users.
  }

  public virtual void BlockUser(string username)
  {
    return; // Forbidden for regular users.
  }

  public virtual void UnblockUser(string username)
  {
    return; // Forbidden for regular users.
  }

  public virtual void ChangeUserType(string username, int type)
  {
    return; // Forbidden for regular users.
  }

  public virtual Task<string> GetAnswer(string messageToReplyTo, string apiKey)
  {
    return Task.FromResult(""); // Using for Robot
  }
}