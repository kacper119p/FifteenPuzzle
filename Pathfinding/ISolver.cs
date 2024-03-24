namespace Pathfinding;

public interface ISolver
{
    public PathfindingData Solve(State start, State goal);
}
