namespace AOC_2021;

public static class DaySeven
{
  public static int MinCrabFuel(string[] inputs)
  {
    var intBuffer = 0;
    var crabPositions = inputs[0].Split(",").SelectMany(stringPosition =>
    {
      if (int.TryParse(stringPosition, out intBuffer))
      {
        return new int[] { intBuffer };
      }
      else
      {
        Console.WriteLine("Could not parse input: " + stringPosition);
        return new int[] { };
      }
    });

    Console.WriteLine(crabPositions.Aggregate(string.Empty, (acc, val) => acc += val + ","));

    return Enumerable.Range(crabPositions.Min(), crabPositions.Max() - crabPositions.Min() + 1)
                              .Min(val => crabPositions.Sum(pos => (Math.Abs(pos - val)).Factorial()));
  }

  public static int Factorial(this int n) => Enumerable.Range(1, n).Aggregate(0, (acc, val) => acc += val);
}
