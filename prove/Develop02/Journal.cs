using System;
using System.IO;

class Journal
{
  public readonly List<Entry> _entries = new List<Entry>();
  public bool _hasUnsaved = false;

  static readonly ColorConsole _console = new ColorConsole();

  public void AddEntry(Entry newEntry)
  {
    _entries.Add(newEntry);
  }

  public void DisplayAll()
  {
    if (_entries.Count == 0)
    {
      _console.RedMsg("Your Journal is empty. Try to write something or load from the file.\n");
      return;
    }

    foreach (Entry entry in _entries)
    {
      Console.WriteLine($"{entry.MakeString()}\n");
    }
  }
}