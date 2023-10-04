namespace AOC_2021;
public static class DayOne
{
  public static int DepthIncreases(string[] depths)
  {
    int result = 0;
    int? prevThreeDepthSum = null;

    for (int i = 0; i <= depths.Length - 3; i++)
    {
      int threeDepthSum = 0;

      int j = 0;
      while (j < 3)
      {
        int depth = 0;
        if (int.TryParse(depths[i + j], out depth))
        {
          threeDepthSum += depth;
        }
        else
        {
          prevThreeDepthSum = null;
          break;
        }

        j++;
      }

      if (prevThreeDepthSum is not null)
      {
        if (threeDepthSum > prevThreeDepthSum)
        {
          result++;
        }
      }
      prevThreeDepthSum = threeDepthSum;
    }

    return result;
  }
}
