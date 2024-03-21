namespace Pathfinding.Heuristics;

public interface IHeuristic
{
    public int MoveCost { get; }
    public int Evaluate(State a, State b);
}
