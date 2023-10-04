namespace AOC_2021;
public static class DayTwo
{
  public static int FinalDepthHorizontalProduct(string[] commands)
  {
    var result = ExecuteSubCommands(commands);

    return result.Depth * result.HorizontalDistance;
  }

  public static SubLocation ExecuteSubCommands(string[] commands)
  {
    var result = new SubLocation()
    {
      HorizontalDistance = 0,
      Depth = 0
    };

    foreach (var command in commands)
    {
      var subStrings = command.Split(' ');

      Keyword keyword = Keyword.none;
      int magnitude = 0;
      if (!int.TryParse(subStrings[1], out magnitude)
          || !Enum.TryParse(subStrings[0], out keyword)
          || !result.ApplyKeyword(keyword, magnitude))
      {
        Console.WriteLine("Could not execute command: " + command);

        return new SubLocation()
        {
          HorizontalDistance = -1,
          Depth = -1
        };
      }
    }

    return result;
  }
}

public class SubLocation
{
  public int HorizontalDistance;
  public int Depth;
  public int Aim = 0;

  public bool ApplyKeyword(Keyword keyword, int magnitude)
  {
    switch (keyword)
    {
      case Keyword.up:
        this.Aim -= magnitude;
        return true;
      case Keyword.down:
        this.Aim += magnitude;
        return true;
      case Keyword.forward:
        this.HorizontalDistance += magnitude;
        this.Depth += magnitude * this.Aim;
        return true;
      default:
        return false;
    }
  }
}

public enum Keyword
{
  none,
  up,
  down,
  forward
}
