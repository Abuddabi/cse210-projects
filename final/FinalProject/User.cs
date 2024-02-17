class User
{
  private string _username;
  private int _type; // 1 - regular user, 2 - moderator, 3 - admin

  public User(string username)
  {
    _username = username;
    _type = 1;
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