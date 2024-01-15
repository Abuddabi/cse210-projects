using System;

class Entry
{
  public string _date;
  public string _promptText;
  public string _entryText;

  public string MakeString()
  {
    string entryString = $"Date: {_date} - Prompt: {_promptText}\n{_entryText}";
    return entryString;
  }
}