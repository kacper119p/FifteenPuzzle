namespace Pathfinding.Heuristics;

public interface IHeuristic
{
    /// <summary>
    /// Cost of single move.
    /// </summary>
    public int MoveCost { get; }

    /// <summary>
    /// Calculates distance between two states.
    /// </summary>
    /// <param name="a">First state</param>
    /// <param name="b">Second state</param>
    /// <returns>Distance between two states</returns>
    public int Evaluate(State a, State b);
}
