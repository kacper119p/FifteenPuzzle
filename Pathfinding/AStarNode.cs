using Pathfinding.Heuristics;

namespace Pathfinding;

internal class AStarNode : Node
{
    private readonly long _cost;
    private readonly long _pathLength;
    private readonly long _distanceToTarget;
    
    public long Cost => _cost;
    public long PathLength => _pathLength;
    public long DistanceToTarget => _distanceToTarget;
    
    public AStarNode(State state, Node cameFrom, Direction move) : base(state, cameFrom, move)
    {
        _pathLength = 0;
        _distanceToTarget = 0;
        _cost = 0;
    }

    public AStarNode(State state, Node cameFrom, Direction move, long pathLength, long distanceToTarget)
        : base(state, cameFrom, move)
    {
        _pathLength = pathLength;
        _distanceToTarget = distanceToTarget;
        _cost = _pathLength + _distanceToTarget;
    }

    public AStarNode(AStarNode parent, Direction direction, long moveCost, State target, IHeuristic heuristic)
        : base(parent.State.StateFromMove(direction), parent, direction)
    {
        _pathLength = parent._pathLength + moveCost;
        _distanceToTarget = heuristic.Evaluate(this.State, target);
        _cost = _pathLength + _distanceToTarget;
    }
}
