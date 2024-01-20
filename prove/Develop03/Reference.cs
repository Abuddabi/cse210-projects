using System;

class Reference
{
  private string _book;
  private int _chapter;
  private int _verse;
  private int? _endVerse;

  public Reference(string book, int chapter, int verse)
  {
    _book = book;
    _chapter = chapter;
    _verse = verse;
  }

  public Reference(string book, int chapter, int startVerse, int endVerse)
  {
    _book = book;
    _chapter = chapter;
    _verse = startVerse;
    _endVerse = endVerse;
  }

  public string GetDisplayText()
  {
    string verses = _endVerse.HasValue ? $"{_verse}-{_endVerse}" : _verse.ToString();
    return $"{_book} {_chapter}:{verses}";
  }
}