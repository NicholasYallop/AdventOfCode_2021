namespace AOC_2021;
class Program
{
  static int Main(string[] args)
  {
    if (!(args.Length > 0))
    {
      Console.WriteLine("Provide filpath to be read as first argument.");
      Exit(1);
      return -1;
    }

    string filePath = args[0];

    string[]? inputs = null;

    int result = 0;

    try
    {
      inputs = File.ReadAllLines(filePath);
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      Exit(1);
      return -1;
    }

    result = DayOne.DepthIncreases(inputs);

    Exit(0);
    return result;
  }

  static int Exit(int code)
  {
    Environment.ExitCode = code;
    return code;
  }
}




