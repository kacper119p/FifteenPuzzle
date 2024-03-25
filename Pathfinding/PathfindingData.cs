namespace Pathfinding;

public struct PathfindingData
{
    public LinkedList<Direction>? solution;
    public long solutionLength;
    public long statesVisited;
    public long statesProcessed;
    public long maxDepth;
    public double processingTimeMilliseconds;
}
