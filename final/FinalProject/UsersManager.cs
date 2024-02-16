class UsersManager
{
  private List<User> _users;
  private AuthManager _authManager = new AuthManager();
  private ConsoleHelper _console = new ConsoleHelper();
  private string _delimiter = "~|~";
  private string _fileName = "users.txt";
  private User _currentUser;

  public UsersManager()
  {
    _users = new List<User>();
  }

  public UsersManager(List<User> users)
  {
    _users = users;
  }

  public List<User> LoadUsers()
  {
    // in future we can change it to database
    string[] lines = File.ReadAllLines(_fileName);

    string[] parts;
    string username;
    string password;
    User user;
    for (int i = 0, l = lines.Length; i < l; i++)
    {
      parts = lines[i].Split(_delimiter);
      username = parts[0];
      password = parts[1];
      _authManager.SetPassword(username, password);

      user = new User(username);
      _users.Add(user);
    }

    return _users;
  }

  public List<User> GetUsers()
  {
    return _users;
  }

  public bool Login()
  {
    string username = _console.GetStringFromUser("\nPlease, write your username: ");
    if (!IsUserExists(username))
    {
      _console.RedMsg($"User {username} doesn't exist. Try to signup.");
      return false;
    }

    bool passwordValid;
    do
    {
      string password = _console.GetStringFromUser("\nPlease, write password: ");
      passwordValid = _authManager.AuthenticateUser(username, password);
    } while (!passwordValid);

    _currentUser = new User(username);

    return true;
  }

  public bool Signup()
  {
    string username = _console.GetStringFromUser("\nPlease, write your username: ");
    if (IsUserExists(username))
    {
      _console.RedMsg($"User {username} already exists. Try to login.");
      return false;
    }
    User newUser = new User(username);
    _users.Add(newUser);
    string password = _console.GetStringFromUser("\nPlease, write password: ");
    _authManager.SetPassword(username, password);
    SaveUser(username, password);

    _currentUser = newUser;

    return true;
  }

  private void SaveUser(string username, string password)
  {
    using (StreamWriter outputFile = new StreamWriter(_fileName))
    {
      outputFile.WriteLine($"{username}{_delimiter}{password}");
    }
  }

  public bool IsUserExists(string username)
  {
    foreach (User user in _users)
    {
      if (user.GetUsername() == username)
        return true;
    }
    return false;
  }

  public void PrintUsers(bool withoutCurrent = false)
  {
    List<User> users = _users;

    if (withoutCurrent && _currentUser != null)
    {
      string currentUsername = _currentUser.GetUsername();
      users = _users.Where(user => user.GetUsername() != currentUsername).ToList();
    }

    foreach (User user in users)
    {
      Console.WriteLine(user.GetUsername());
    }
  }
}