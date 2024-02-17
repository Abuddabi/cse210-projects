class UsersManager
{
  private List<User> _users;
  private AuthManager _authManager = new AuthManager();
  private ConsoleHelper _console = new ConsoleHelper();
  private FileHandler _fileHandler = new FileHandler();
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

  public void LoadUsers()
  {
    // in future we can change it to database
    string[] lines = File.ReadAllLines(_fileName);
    string delimiter = _fileHandler.GetDelimiter();

    string[] parts;
    string username;
    string password;
    int type;
    bool isBlocked;
    User user;
    for (int i = 0, l = lines.Length; i < l; i++)
    {
      parts = lines[i].Split(delimiter);
      username = parts[0];
      password = parts[1];
      type = int.Parse(parts[2]);
      isBlocked = bool.Parse(parts[3]);
      _authManager.SetPassword(username, password);

      user = new User(username, type, isBlocked);
      _users.Add(user);
    }
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
      string password = _console.GetStringFromUser("Please, write password: ");
      passwordValid = _authManager.AuthenticateUser(username, password);
      if (!passwordValid)
        _console.RedMsg("Password is invalid. Try one more time.");
    } while (!passwordValid);

    _currentUser = GetUserByUsername(username);

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
    string password = _console.GetStringFromUser("Please, write password: ");
    _authManager.SetPassword(username, password);
    SaveUser(newUser, password);

    _currentUser = newUser;

    return true;
  }

  private void SaveUser(User user, string password)
  {
    string delimiter = _fileHandler.GetDelimiter();
    string testForFile = $"{user.GetUsername()}{delimiter}{password}{delimiter}{user.GetUserType()}";
    _fileHandler.AppendToFile(_fileName, testForFile);
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

  public User GetCurrentUser()
  {
    return _currentUser;
  }

  public User GetUserByUsername(string username)
  {
    return _users.FirstOrDefault(u => u.GetUsername() == username);
  }
}