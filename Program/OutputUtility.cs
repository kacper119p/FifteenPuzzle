using System.Globalization;
using Pathfinding;

namespace Program;

public static class OutputUtility
{
    public static char DirectionToChar(Direction direction)
    {
        return direction switch
        {
            Direction.Up => 'U',
            Direction.Down => 'D',
            Direction.Left => 'L',
            Direction.Right => 'R',
            _ => throw new ArgumentException("Invalid Direction")
        };
    }

    public static void OutputSolution(string path, PathfindingData data)
    {
        using FileStream file = new FileStream(path, FileMode.Create);
        using StreamWriter writer = new StreamWriter(file);
        writer.WriteLine(data.solutionLength);
        foreach (Direction direction in data.solution)
        {
            writer.Write(DirectionToChar(direction));
        }
    }

    public static void OutputStats(string path, PathfindingData data)
    {
        NumberFormatInfo numberFormatInfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberDecimalDigits = 3
        };

        using FileStream file = new FileStream(path, FileMode.Create);
        using StreamWriter writer = new StreamWriter(file);
        writer.WriteLine(data.solutionLength);
        writer.WriteLine(data.statesVisited);
        writer.WriteLine(data.statesProcessed);
        writer.WriteLine(data.maxDepth);
        writer.WriteLine(string.Format(numberFormatInfo, "{0:N}", data.processingTimeMilliseconds));
    }
}
