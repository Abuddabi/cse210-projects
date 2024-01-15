using System;

class FileHandler
{

  static readonly string _delimiter = "~|~";

  public readonly string[] _validFileExtensions = {
    ".txt",
    ".md"
  };
  static Journal _journal = new Journal();
  static readonly ColorConsole _console = new ColorConsole();

  public void SaveToFile(string fileName)
  {
    string d;
    string text;

    using (StreamWriter outputFile = new StreamWriter(fileName))
    {
      foreach (Entry entry in _journal._entries)
      {
        d = _delimiter;
        text = $"{entry._date}{d}{entry._promptText}{d}{entry._entryText}";
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

      _journal.AddEntry(newEntry);
    }

    _console.GreenMsg("Your Journal has been successfully loaded.\n");
  }

  public bool CheckFile(string fileName)
  {
    bool isValid = false;

    foreach (string ext in _validFileExtensions)
    {
      if (fileName.Contains(ext))
      {
        isValid = true;
        break;
      }
    }

    return isValid;
  }
}