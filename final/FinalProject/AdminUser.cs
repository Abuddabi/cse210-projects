class AdminUser : ModeratorUser
{
  public AdminUser(string username) : base(username)
  {

  }

  public AdminUser(string username, int type, bool isBlocked) : base(username, type, isBlocked)
  {

  }

  public override string GetTypeGreeting()
  {
    return " You're Admin.";
  }

  public override void SetUsersManager(UsersManager usersManager)
  {
    _usersManager = usersManager;
  }

  public override void BlockUser(string username)
  {
    _usersManager.BlockUser(username);
  }

  public override void UnblockUser(string username)
  {
    _usersManager.UnblockUser(username);
  }

  public override void ChangeUserType(string username, int type)
  {
    _usersManager.ChangeUserType(username, type);
  }
}