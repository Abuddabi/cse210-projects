class Message
{
  private User _sender;
  private string _content;
  private string _date;

  public Message(User sender, string content)
  {
    _sender = sender;
    _content = content;
    _date = DateTime.Now.ToString("dd MMM HH:mm");
  }

  public string GetContent()
  {
    return _content;
  }

  public string GetChatLine()
  {
    return $"{_date} {_sender.GetUsername()}: {_content}";
  }
}