using Pathfinding.Heuristics;

namespace Pathfinding;

public class AStarNode : Node
{
    private long _cost;

    public long Cost => _cost;

    public AStarNode(State state, Node cameFrom, Direction move, long cost) : base(state, cameFrom, move)
    {
        _cost = cost;
    }

    public AStarNode AStarNodeFromMove
        (AStarNode parent, Direction direction, long moveCost, State target, IHeuristic heuristic)
    {
        AStarNode result = new AStarNode(parent.State.StateFromMove(direction), parent, direction, 0);
        result._cost = parent.Cost + moveCost + heuristic.Evaluate(result.State, target);
        return result;
    }
}
