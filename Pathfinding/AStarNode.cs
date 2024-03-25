using Pathfinding.Heuristics;

namespace Pathfinding;

internal class AStarNode : Node
{
    private readonly ulong _cost;
    private readonly ulong _pathLength;
    private readonly ulong _distanceToTarget;

    public ulong Cost => _cost;
    public ulong PathLength => _pathLength;
    public ulong DistanceToTarget => _distanceToTarget;
    
    public AStarNode(State state) : base(state)
    {
        _pathLength = 0;
        _distanceToTarget = 0;
        _cost = 0;
    }

    public AStarNode(State state, Node? cameFrom, Direction move) : base(state, cameFrom, move)
    {
        _pathLength = 0;
        _distanceToTarget = 0;
        _cost = 0;
    }

    public AStarNode(State state, Node? cameFrom, Direction move, ulong pathLength, ulong distanceToTarget)
        : base(state, cameFrom, move)
    {
        _pathLength = pathLength;
        _distanceToTarget = distanceToTarget;
        _cost = _pathLength + _distanceToTarget;
    }

    public AStarNode(AStarNode parent, Direction direction, State target, IHeuristic heuristic)
        : base(parent.State.StateFromMove(direction), parent, direction)
    {
        _pathLength = parent._pathLength + (ulong)heuristic.MoveCost;
        _distanceToTarget = (ulong)heuristic.Evaluate(this.State, target);
        _cost = _pathLength + _distanceToTarget;
    }

    public AStarNode NodeFromMove(Direction direction, IHeuristic heuristic, State goal)
    {
        State state = this.State.StateFromMove(direction);
        return new AStarNode(
            state,
            this,
            direction,
            this._pathLength + (ulong)heuristic.MoveCost,
            (ulong)heuristic.Evaluate(state, goal));
    }
}
