using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

class FileHandler
{

  static readonly string _delimiter = ",";

  public readonly string[] _validFileExtensions = {
    ".csv"
  };
  static readonly ColorConsole _console = new ColorConsole();

  public static void SaveToFile(string fileName)
  {
    string d = _delimiter;
    string text;
    string prompt;
    string entryText;
    List<Entry> entries = Journal.GetEntries();
    StringBuilder output = new StringBuilder();

    output.AppendLine($"Date{d}Prompt text{d}Entry text");

    foreach (Entry entry in entries)
    {
      prompt = PrepareForCSV(entry._promptText);
      entryText = PrepareForCSV(entry._entryText);

      text = $"{entry._date}{d}{prompt}{d}{entryText}";
      output.AppendLine(text);
    }

    try
    {
      File.WriteAllText(fileName, output.ToString());
      _console.GreenMsg($"Your Journal has been successfully saved to {fileName} file.\n");
    }
    catch (UnauthorizedAccessException)
    {
      _console.RedMsg($"Access to the file {fileName} is unauthorized.");
    }
    catch (PathTooLongException)
    {
      _console.RedMsg($"The path to the file {fileName} is too long.");
    }
    catch (IOException ex)
    {
      _console.RedMsg($"An error occurred while writing to the file {fileName}. Error: {ex}");
    }
    catch (Exception ex)
    {
      _console.RedMsg($"An unexpected error occurred. Error: {ex}");
    }
  }

  public static void LoadFromFile(string fileName)
  {
    using (TextFieldParser parser = new TextFieldParser(fileName))
    {
      parser.SetDelimiters(_delimiter);

      // Skip the first line with headings
      parser.ReadLine();

      while (!parser.EndOfData)
      {
        string[] fields = parser.ReadFields();

        Entry newEntry = new Entry()
        {
          _date = fields[0],
          _promptText = fields[1],
          _entryText = fields[2]
        };

        Journal.AddEntry(newEntry);
      }
    }

    _console.GreenMsg("Your Journal has been successfully loaded.\n");
  }

  public bool CheckExtension(string fileName)
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

  static string PrepareForCSV(string text)
  {
    if (text.Contains("\""))
    {
      text = text.Replace("\"", "\"\"");
      text = string.Format("\"{0}\"", text);
    }
    else if (text.Contains(",") || text.Contains(Environment.NewLine))
    {
      text = string.Format("\"{0}\"", text);
    }

    return text;
  }
}