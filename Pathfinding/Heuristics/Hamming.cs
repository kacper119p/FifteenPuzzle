using System.Diagnostics;

namespace Pathfinding.Heuristics;

public class Hamming : IHeuristic
{
    public int MoveCost => 1;

    public int Evaluate(State a, State b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
        {
            throw new ArgumentException("Sizes do not match");
        }

        int distance = 0;
        for (int x = 0; x < a.Height; x++)
        {
            for (int y = 0; y < a.Width; y++)
            {
                if (a[x, y] != b[x, y] && a[x, y] != 0)
                {
                    distance++;
                }
            }
        }

        return distance;
    }
}
