namespace AOC_2021;
public static class DayFour
{
  public static int GetBestBingoScore(string[] data)
  {
    var numbers = GetNumbers(data[0]);

    List<BingoSquare[][]> boards = new List<BingoSquare[][]>();

    BingoSquare[][] board = new BingoSquare[0][];

    for (int i = 2; i < data.Length; i++)
    {
      if (data[i].Length == 0 && board.Length != 0)
      {
        boards.Add(board);
        board = new BingoSquare[0][];
        continue;
      }

      board = board.Append(data[i].Split(" ").Where(s => s.Length > 0).Select(s => new BingoSquare()
      {
        digit = int.Parse(s)
      }).ToArray()).ToArray();
    }

    if (board.Length != 0) boards.Add(board);

    Console.WriteLine("There are " + boards.Count + " boards");

    var numberEnumerator = numbers.GetEnumerator();
    while (true)
    {
      if (!numberEnumerator.MoveNext())
      {
        Console.WriteLine("RAN OUT OF NUMBERS");
        foreach (var b in boards)
        {
          b.Print();
        }
        return 0;
      }
      else
      {
        Console.WriteLine("Enumerated " + numberEnumerator.Current);
      }

      foreach (var candidate in boards)
      {
        foreach (var row in candidate)
        {
          foreach (var square in row)
          {
            if (square.digit == numberEnumerator.Current) square.ticked = true;
          }
        }
      }

      if (boards.Count != 1)
      {
        boards = boards.Where(b => !b.IsWinner()).ToList();
      }
      else if (boards[0].IsWinner())
      {
        break;
      }
    }

    foreach (var b in boards)
    {
      Console.WriteLine("Board:");
      b.Print();
    }
    var result = 0;

    foreach (var row in boards[0])
    {
      foreach (var square in row)
      {
        if (!square.ticked) result += square.digit;
      }
    }

    return result * numberEnumerator.Current;
  }

  private static IEnumerable<int> GetNumbers(string csv)
  {
    int result = 0;
    string buffer = string.Empty;

    foreach (var character in csv)
    {
      if (character == ',')
      {
        if (int.TryParse(buffer, out result))
        {
          yield return result;
        }

        buffer = string.Empty;
      }

      else
      {
        buffer += character;
      }
    }

    if (int.TryParse(buffer, out result))
    {
      yield return result;
      buffer = string.Empty;
    }
  }

  private static bool IsWinner(this BingoSquare[][] board)
  {
    for (int i = 0; i < board.Length; i++)
    {
      if (board[i].All(b => b.ticked)) return true;
    }

    for (int i = 0; i < board[0].Length; i++)
    {
      for (int j = 0; j < board.Length; j++)
      {
        if (!board[j][i].ticked) break;
        if (j == board.Length - 1) return true;
      }
    }

    return false;
  }

  private static void Print(this BingoSquare[][] board)
  {
    foreach (var row in board)
    {
      Console.WriteLine(row.Aggregate(string.Empty, (agg, square) =>
      {
        if (square.ticked)
        {
          return agg + "     *" + square.digit;
        }
        else
        {
          return agg + "     " + square.digit;
        }
      }));
    }
  }
}

public class BingoSquare
{
  public int digit;
  public bool ticked;
}
