namespace AOC_2021;

public static class DayFive
{
  public static int OverlappingVents(string[] inputs)
  {
    List<VentLine> lines = new List<VentLine>();

    foreach (var input in inputs)
    {
      var splits = input.Split(" ");
      var firstPoint = splits[0].Split(",");
      var secondPoint = splits[2].Split(",");
      int x1 = 0;
      int y1 = 0;
      int x2 = 0;
      int y2 = 0;

      if (int.TryParse(firstPoint[0], out x1)
          && int.TryParse(firstPoint[1], out y1)
          && int.TryParse(secondPoint[0], out x2)
          && int.TryParse(secondPoint[1], out y2)
          )
      {
        lines.Add(new VentLine(x1, y1, x2, y2));
      }
      else
      {
        Console.WriteLine("Failed to parse line: " + input);
      }
    }

    Dictionary<TwoVector, int> seaFloor = new Dictionary<TwoVector, int>();
    TwoVector locationBuffer = new TwoVector();

    foreach (var line in lines)
    {
      Console.WriteLine($"{line.Start.x},{line.Start.y} -> {line.End.x},{line.End.y}");

      if (line.Start.x != line.End.x && line.Start.y != line.End.y)
      {
        int i = line.Start.x;
        Action incrementI;
        Predicate<int> iStillIterating;
        if (line.Start.x < line.End.x)
        {
          incrementI = new Action(() => i++);
          iStillIterating = new Predicate<int>(i => i <= line.End.x);
        }
        else
        {
          incrementI = new Action(() => i--);
          iStillIterating = new Predicate<int>(i => i >= line.End.x);
        }

        int j = line.Start.y;
        Action incrementJ;
        Predicate<int> jStillIterating;
        if (line.Start.y < line.End.y)
        {
          incrementJ = new Action(() => j++);
          jStillIterating = new Predicate<int>(j => j <= line.End.y);
        }
        else
        {
          incrementJ = new Action(() => j--);
          jStillIterating = new Predicate<int>(j => j >= line.End.y);
        }

        while (iStillIterating(i) && jStillIterating(j))
        {
          Console.WriteLine($"{i},{j}");

          locationBuffer.x = i;
          locationBuffer.y = j;
          if (seaFloor.ContainsKey(locationBuffer))
          {
            seaFloor[locationBuffer] += 1;
          }
          else
          {
            seaFloor.Add(new TwoVector()
            {
              x = i,
              y = j
            }, 1);
          }

          incrementI();
          incrementJ();
        }

        continue;
      }

      int jMax = Math.Max(line.Start.y, line.End.y);
      int iMax = Math.Max(line.Start.x, line.End.x);

      for (int i = Math.Min(line.Start.x, line.End.x); i <= iMax; i++)
      {

        for (int j = Math.Min(line.Start.y, line.End.y); j <= jMax; j++)
        {
          Console.WriteLine($"{i},{j}");

          locationBuffer.x = i;
          locationBuffer.y = j;
          if (seaFloor.ContainsKey(locationBuffer))
          {
            seaFloor[locationBuffer] += 1;
          }
          else
          {
            seaFloor.Add(new TwoVector()
            {
              x = i,
              y = j
            }, 1);
          }
        }
      }
    }

    return seaFloor.Count(pair => pair.Value > 1);
  }
}

public class VentLine
{
  public TwoVector Start;
  public TwoVector End;
  public TwoVector Direction;

  public VentLine(int x1, int y1, int x2, int y2)
  {
    this.Start = new TwoVector()
    {
      x = x1,
      y = y1
    };
    this.End = new TwoVector()
    {
      x = x2,
      y = y2
    };

    this.Direction = new TwoVector()
    {
      x = x2 - x1,
      y = y2 - y1
    };
  }

  /*
  public Intersect Intersects(VentLine line)
  {
    if (this.Direction.CrossProduct(line.Direction) == 0)
    {
      // parallel
      if ((this.Start - line.Start).CrossProduct(this.Direction) == 0)
      {
        // would overlap if infinite
        if (this.Start.LiesOnVentLine(line)
            || this.End.LiesOnVentLine(line)
            || line.Start.LiesOnVentLine(this)
            || line.End.LiesOnVentLine(this))
        {
          return Intersect.overlaps;
        }
        else
        {
          return Intersect.none;
        }
      }
    };
    // would intersect if infinite

  }*/

  public static bool operator ==(VentLine line1, VentLine line2) => line1.Start == line2.Start && line1.End == line2.End;

  public static bool operator !=(VentLine line1, VentLine line2) => line1.Start != line2.Start || line1.End != line2.End;

  public override int GetHashCode()
  {
    return base.GetHashCode();
  }

  public override bool Equals(object? obj)
  {
    var line = obj as VentLine;
    if (line is null) return false;
    return this == line;
  }
}

public class TwoVector
{
  public int x;
  public int y;

  public static TwoVector operator -(TwoVector vec1, TwoVector vec2) => new TwoVector()
  {
    x = vec1.x - vec2.x,
    y = vec1.y - vec2.y
  };

  public int SquareMagnitude => this.x * this.x + this.y * this.y;

  public static bool operator ==(TwoVector vec1, TwoVector vec2) => vec1.x == vec2.x && vec1.y == vec2.y;

  public static bool operator !=(TwoVector vec1, TwoVector vec2) => vec1.x != vec2.x || vec1.y != vec2.y;

  public override int GetHashCode()
  {
    return HashCode.Combine(x.GetHashCode(), y.GetHashCode());
  }

  public override bool Equals(object? obj)
  {
    var vec = obj as TwoVector;
    if (vec is null) return false;
    return this == vec;
  }

  public bool LiesOnVentLine(VentLine line) => (this - line.Start).CrossProduct(line.Direction) == 0;

  public int CrossProduct(TwoVector vec)
  {
    return this.x * vec.y - this.y * vec.x;
  }
}

public enum Intersect
{
  none,
  intersects,
  overlaps
}

