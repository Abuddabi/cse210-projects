class Message
{
  private User _sender;
  private string _content;
  private DateTime _timestamp;

  public Message(User sender, string content)
  {
    _sender = sender;
    _content = content;
    _timestamp = DateTime.Now;
  }

  public string GetContent()
  {
    return _content;
  }
}