using System;

namespace programming_project.utils
{
  public class Table
  {

    public void PrintLine()
    {
      Console.WriteLine(new string('-', 77));
    }

    public void PrintRow(params string[] columns)
    {
      int width = (77 - columns.Length) / columns.Length;
      string row = "|";
      foreach (string column in columns)
      {
        row += AlignCentre(column, width) + "|";
      }
      Console.WriteLine(row);

    }

    public string AlignCentre(string text, int width)
    {
      text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
      if (string.IsNullOrEmpty(text))
      {
        return new string(' ', width);
      }
      else
      {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
      }
    }
  }
}