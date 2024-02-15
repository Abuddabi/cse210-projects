class Message
{
  private User _sender;
  private User _recipient;
  private string _content;
  private DateTime _timestamp;

  public Message(User sender, User recipient, string content)
  {
    _sender = sender;
    _recipient = recipient;
    _content = content;
    _timestamp = DateTime.Now;
  }

  public User GetRecipient()
  {
    return _recipient;
  }

  public string GetContent()
  {
    return _content;
  }
}