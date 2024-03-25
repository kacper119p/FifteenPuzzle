namespace Pathfinding.Exceptions;

public class SolutionNotFoundException : Exception
{
    private PathfindingData _data;
    public PathfindingData Data => _data;

    public SolutionNotFoundException(PathfindingData data)
    {
        _data = data;
    }
}
