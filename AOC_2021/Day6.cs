namespace AOC_2021;

public static class DaySix
{
  public static int EightyDaysOfFish(string[] inputs)
  {
    Dictionary<int, ulong> fishTimerCounts = new Dictionary<int, ulong>(){
      {0,0},
      {1,0},
      {2,0},
      {3,0},
      {4,0},
      {5,0},
      {6,0},
      {7,0},
      {8,0}
    };

    int intBuffer = 0;
    foreach (var stringNum in inputs[0].Split(","))
    {
      if (int.TryParse(stringNum, out intBuffer)) fishTimerCounts[intBuffer]++;
      else Console.WriteLine("Could not parse number: " + stringNum);
    }

    for (int t = 1; t <= 256; t++)
    {
      ulong currentValue = 0;
      ulong previousValue = 0;
      for (int i = 8; i >= 0; i--)
      {
        currentValue = fishTimerCounts[i];
        fishTimerCounts[i] = previousValue;
        previousValue = currentValue;
      }

      fishTimerCounts[6] += previousValue;
      fishTimerCounts[8] = previousValue;
    }

    Console.WriteLine(fishTimerCounts.ToList().Aggregate(0UL, (acc, val) => acc += val.Value));
    return 0;
  }
}
