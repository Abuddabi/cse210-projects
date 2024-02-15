class User
{
  private string _username;

  public User(string username)
  {
    _username = username;
  }

  public string GetUsername()
  {
    return _username;
  }
}