class ChatRoom
{
  private List<Message> _messages;
  private string _name;

  public ChatRoom(string name)
  {
    _messages = new List<Message>();
    _name = name;
  }

  public void SendMessage(User sender, string content)
  {
    Message message = new Message(sender, content);
    _messages.Add(message);
  }

  public List<Message> GetMessages()
  {
    return _messages;
  }

  public string GetName()
  {
    return _name;
  }
}