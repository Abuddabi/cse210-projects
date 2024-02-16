class Message
{
  private User _sender;
  private string _content;
  private DateTime _timestamp;
  private FileHandler _fileHandler = new FileHandler();

  public Message(User sender, string content)
  {
    _sender = sender;
    _content = content;
    _timestamp = DateTime.Now;
  }

  public Message(User sender, string content, string date)
  {
    _sender = sender;
    _content = content;
    _timestamp = DateTime.Parse(date);
  }

  public string GetContent()
  {
    return _content;
  }

  public string GetChatLine()
  {
    return $"{FormatDate()} {_sender.GetUsername()}: {_content}";
  }

  private string FormatDate()
  {
    return _timestamp.ToString("dd MMM HH:mm");
  }

  public string GetTextForFile()
  {
    string delimiter = _fileHandler.GetDelimiter();
    string dateStr = _timestamp.ToString("o");
    return $"{dateStr}{delimiter}{_sender.GetUsername()}{delimiter}{_content}";
  }
}