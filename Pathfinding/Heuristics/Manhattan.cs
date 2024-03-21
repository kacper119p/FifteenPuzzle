using System.Diagnostics;

namespace Pathfinding.Heuristics;

public class Manhattan : IHeuristic
{
    public int MoveCost { get => 2; }
    
    public int Evaluate(State a, State b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
        {
            throw new ArgumentException("Sizes do not match");
        }

        Point<int>[] positions = new Point<int>[a.Height * a.Width];

        for (int x = 0; x < a.Height; x++)
        {
            for (int y = 0; y < a.Width; y++)
            {
                positions[a[x, y]] = new Point<int>(x, y);
            }
        }

        int distance = 0;
        for (int x = 0; x < a.Height; x++)
        {
            for (int y = 0; y < a.Width; y++)
            {
                distance += Distance(positions[b[x, y]], new Point<int>(x, y));
            }
        }
        return distance;
    }

    private static int Distance(Point<int> a, Point<int> b) =>
        Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
}
