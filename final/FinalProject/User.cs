class User
{
  private string _username;
  private int _type; // 1 - regular user, 2 - moderator, 3 - admin
  private bool _isBlocked;

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

  protected virtual void ChangeUserType(int type)
  {
    _type = type;
  }

  public string GetUsername()
  {
    return _username;
  }

  public int GetUserType()
  {
    return _type;
  }
}