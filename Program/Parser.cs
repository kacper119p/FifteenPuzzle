using Pathfinding;
using Pathfinding.Heuristics;

namespace Program;

public static class Parser
{
    public static IHeuristic ParseHeuristic(string heuristic)
        => heuristic switch
        {
            "manh" => new Manhattan(),
            "hamm" => new Hamming(),
            _ => throw new ParserException("Invalid heuristic")
        };


    public static State ParseInputFile(string path)
    {
        using StreamReader reader = new StreamReader(path);
        string row = reader.ReadLine() ?? throw new ParserException("Invalid Data");
        string[] separated = row.Split(' ');
        if (separated.Length != 2)
        {
            throw new ParserException("Invalid Data");
        }

        if (!int.TryParse(separated[0], out int rows))
        {
            throw new ParserException("Invalid Data");
        }

        if (!int.TryParse(separated[1], out int columns))
        {
            throw new ParserException("Invalid Data");
        }

        byte[,] fields = new byte[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            row = reader.ReadLine() ?? throw new ParserException();
            separated = row.Split(' ');
            if (separated.Length != columns)
            {
                throw new ParserException("Invalid Data");
            }

            for (int y = 0; y < columns; y++)
            {
                if (!byte.TryParse(separated[y], out byte value))
                {
                    throw new ParserException("Invalid Data");
                }

                fields[x, y] = value;
            }
        }

        try
        {
            State result = new State(fields);
            return result;
        }
        catch (ArgumentException e)
        {
            throw new ParserException("No zero in fields");
        }
    }

    public static Strategy ParseStrategy(string strategy)
        => strategy switch

        {
            "bfs" => Strategy.Bfs,
            "dfs" => Strategy.Dfs,
            "astr" => Strategy.AStar,
            _ => throw new ParserException("Invalid strategy")
        };

    public static SearchOrder ParseSearchOrder(string searchOrder)
    {
        if (searchOrder.Length != SearchOrder.DirectionsCount)
        {
            throw new ParserException("InvalidSearchOrder");
        }

        Direction[] directions = new Direction[SearchOrder.DirectionsCount];

        for (int i = 0; i < SearchOrder.DirectionsCount; i++)
        {
            directions[i] = searchOrder[i] switch
            {
                'U' => Direction.Up,
                'D' => Direction.Down,
                'L' => Direction.Left,
                'R' => Direction.Right,
                _ => throw new ParserException("InvalidSearchOrder")
            };
        }

        return new SearchOrder(directions);
    }

    public class ParserException : Exception
    {
        public ParserException()
        {
        }

        public ParserException(string message) : base(message)
        {
        }
    }
}
