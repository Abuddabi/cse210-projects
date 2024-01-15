using System;
using System.IO;

class Journal
{
  static readonly List<Entry> _entries = new List<Entry>();
  public bool _hasUnsaved = false;

  static readonly string _delimiter = "~|~";

  static readonly ColorConsole _console = new ColorConsole();

  public void AddEntry(Entry newEntry)
  {
    _entries.Add(newEntry);
  }

  public void DisplayAll()
  {
    if (_entries.Count == 0)
    {
      _console.RedMsg("Your Journal is empty. Try to write something.\n");
      return;
    }

    foreach (Entry entry in _entries)
    {
      Console.WriteLine($"{entry.MakeString()}\n");
    }
  }

  public void SaveToFile(string fileName)
  {
    using (StreamWriter outputFile = new StreamWriter(fileName))
    {
      foreach (Entry entry in _entries)
      {
        string d = _delimiter;
        string text = $"{entry._date}{d}{entry._promptText}{d}{entry._entryText}";
        outputFile.WriteLine(text);
      }
    }

    _console.GreenMsg($"Your Journal has been successfully saved to {fileName} file.\n");
  }

  public void LoadFromFile(string fileName)
  {
    string[] lines = File.ReadAllLines(fileName);

    foreach (string line in lines)
    {
      string[] parts = line.Split(_delimiter);

      Entry newEntry = new Entry()
      {
        _date = parts[0],
        _promptText = parts[1],
        _entryText = parts[2]
      };

      AddEntry(newEntry);
    }

    _console.GreenMsg("Your Journal has been successfully loaded.\n");
  }
}