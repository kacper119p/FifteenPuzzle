namespace Pathfinding.Heuristics;

public interface IHeuristic
{
    public int Evaluate(State a, State b);
}
