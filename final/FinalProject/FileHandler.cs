class FileHandler
{
  private string _delimiter;

  public FileHandler()
  {
    _delimiter = "~|~";
  }

  public string GetDelimiter()
  {
    return _delimiter;
  }

  public void AppendToFile(string fileName, string text)
  {
    using (StreamWriter outputFile = new StreamWriter(fileName, true))
    {
      outputFile.WriteLine(text);
    }
  }
}