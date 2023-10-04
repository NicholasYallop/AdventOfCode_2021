namespace AOC_2021;
public static class DayThree
{
  public static int GetLifeSupportRating(string[] binaries)
  {
    var scrubberBinaries = new List<string>(binaries);
    var oxygenBinaries = new List<string>(binaries);

    int i = 0;
    while (oxygenBinaries.Count > 1 && i < oxygenBinaries[0].Length)
    {
      int zeroes = 0;
      int ones = 0;

      foreach (var binary in oxygenBinaries)
      {
        if (binary[i] == '0')
        {
          zeroes += 1;
        }
        else
        {
          ones += 1;
        }
      }

      oxygenBinaries = (zeroes > ones)
        ? oxygenBinaries.Where(x => x[i] == '0').ToList()
        : oxygenBinaries.Where(x => x[i] == '1').ToList();

      i++;
    }

    if (oxygenBinaries.Count > 1)
    {
      Console.WriteLine("Found competing oxygen binaries:" + oxygenBinaries.Aggregate(string.Empty, (agg, str) => agg += " str"));
      return -1;
    }

    i = 0;
    while (scrubberBinaries.Count > 1 && i < scrubberBinaries[0].Length)
    {
      int zeroes = 0;
      int ones = 0;

      foreach (var binary in scrubberBinaries)
      {
        if (binary[i] == '0')
        {
          zeroes += 1;
        }
        else
        {
          ones += 1;
        }
      }

      scrubberBinaries = (zeroes > ones)
        ? scrubberBinaries.Where(x => x[i] == '1').ToList()
        : scrubberBinaries.Where(x => x[i] == '0').ToList();

      i++;
    }

    if (scrubberBinaries.Count > 1)
    {
      Console.WriteLine("Found competing scrubber binaries:" + scrubberBinaries.Aggregate(string.Empty, (agg, str) => agg += " str"));
      return -1;
    }

    return ParseStringAsBinary(oxygenBinaries[0]) * ParseStringAsBinary(scrubberBinaries[0]);
  }

  public static int GetPowerConsumtion(string[] binaries)
  {
    Dictionary<int, binaryCounts> locationMap = new Dictionary<int, binaryCounts>();

    foreach (var binary in binaries)
    {
      for (int i = 0; i < binary.Length; i++)
      {
        if (!locationMap.ContainsKey(i))
        {
          locationMap.Add(i, new binaryCounts());
        }

        switch (binary[i])
        {
          case '0':
            locationMap[i].zeroes += 1;
            break;
          case '1':
            locationMap[i].ones += 1;
            break;
          default:
            Console.WriteLine("Could not process binary: " + binary);
            return -1;
        }
      }
    }

    var gammaString = string.Empty;
    var epsilonString = string.Empty;

    foreach (var locationCounts in locationMap)
    {
      if (locationCounts.Value.ones > locationCounts.Value.zeroes)
      {
        gammaString += "1";
        epsilonString += "0";
      }
      else
      {
        gammaString += "0";
        epsilonString += "1";
      }
    }

    return ParseStringAsBinary(epsilonString) * ParseStringAsBinary(gammaString);
  }

  public static int ParseStringAsBinary(string binary)
  {
    int result = 0;
    int positionValue = 1;

    for (int i = 0; i < binary.Length; i++)
    {
      if (binary[binary.Length - 1 - i] == '1')
      {
        result += positionValue;
      }

      positionValue *= 2;
    }

    return result;
  }
}

public class binaryCounts
{
  public int zeroes;
  public int ones;
}
