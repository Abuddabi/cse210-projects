using System;

class Scripture
{
  private Reference _reference;
  private List<Word> _words = new List<Word>();
  private List<int> _shownWordsIndexes;
  private Random rnd = new Random();

  public Scripture(Reference reference, string text)
  {
    _reference = reference;
    string[] words = text.Split(" ");

    foreach (string word in words)
    {
      if (word.Contains('.'))
      {
        string[] subWords = word.Split(".");

        Word newWord1 = new Word(subWords[0] + ".");
        _words.Add(newWord1);

        if (string.IsNullOrWhiteSpace(subWords[1]))
        {
          continue;
        }

        Word newWord2 = new Word(subWords[1]);
        _words.Add(newWord2);
      }
      else
      {
        Word newWord = new Word(word);
        _words.Add(newWord);
      }
    }

    // we fill _shownWordsIndexes with indexes of _words List.
    _shownWordsIndexes = Enumerable.Range(0, _words.Count).ToList();
  }

  public void HideRandomWords()
  {
    int r;
    int i;

    for (int j = 0; j < 3; j++)
    {
      if (_shownWordsIndexes.Count == 0)
      {
        break;
      }

      r = rnd.Next(_shownWordsIndexes.Count);
      i = _shownWordsIndexes[r];
      _words[i].Hide();
      _shownWordsIndexes.RemoveAt(r);
    }
  }

  public string GetDisplayText()
  {
    string text = _reference.GetDisplayText();

    foreach (Word word in _words)
    {
      text += " " + word.GetDisplayText();
    }

    return text;
  }

  public bool IsCompletelyHidden()
  {
    return _shownWordsIndexes.Count == 0;
  }
}