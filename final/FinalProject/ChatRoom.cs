class ChatRoom
{
  private List<Message> _messages;

  public ChatRoom()
  {
    _messages = new List<Message>();
  }

  public void SendMessage(User sender, User recipient, string content)
  {
    Message message = new Message(sender, recipient, content);
    _messages.Add(message);
  }

  public List<Message> GetMessages()
  {
    return _messages;
  }
}