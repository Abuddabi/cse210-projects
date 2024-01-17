using System;

class Entry
{
  public string _date;
  public string _promptText;
  public string _entryText;

  string EntryToString()
  {
    return $"Date: {_date} - Prompt: {_promptText}\n{_entryText}";
  }

  public void Display()
  {
    string text = EntryToString();
    Console.WriteLine($"{text}\n");
  }
}