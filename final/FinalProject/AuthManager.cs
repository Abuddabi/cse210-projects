class AuthManager
{
  private Dictionary<string, string> _usersPasswords; // temporary measure
  private ConsoleHelper _console;

  public AuthManager()
  {
    _console = new ConsoleHelper();
  }

  public AuthManager(List<User> users)
  {
    _console = new ConsoleHelper();
  }

  public void SetPassword(string username, string password)
  {
    _usersPasswords[username] = password;
  }

  public bool AuthenticateUser(string username, string password)
  {
    if (_usersPasswords.ContainsKey(username))
    {
      return _usersPasswords[username] == password;
    }
    return false;
  }
}