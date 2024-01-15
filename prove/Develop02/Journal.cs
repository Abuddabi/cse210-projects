using System;

class Journal
{
  static readonly List<Entry> _entries = new List<Entry>();
  public bool _hasUnsaved = false;

  public void AddEntry(string prompt, string userText)
  {
    Entry newEntry = new Entry()
    {
      _date = DateTime.Now.ToString("MM/dd/yyyy"),
      _promptText = prompt,
      _entryText = userText
    };

    _entries.Add(newEntry);
  }

  public void DisplayAll()
  {
    foreach (Entry entry in _entries)
    {
      entry.Display();
    }
  }

  public void SaveToFile(string fileName)
  {

  }

  public void LoadFromFile(string fileName)
  {

  }
}