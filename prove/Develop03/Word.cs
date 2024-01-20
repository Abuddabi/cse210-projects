using System;

class Word
{
  private string _text;
  private bool _isHidden;

  public Word(string text)
  {
    _text = text;
    _isHidden = false;
  }

  public void Hide()
  {
    _isHidden = true;
  }

  private void Show()
  {
    _isHidden = false;
  }

  private bool IsHidden()
  {
    return _isHidden;
  }

  public string GetDisplayText()
  {
    return _isHidden ? new string('_', _text.Length) : _text;
  }
}