namespace Pathfinding;

public class Node
{
    private readonly State _state;
    private readonly Node _cameFrom;
    private readonly Direction _move;

    public State State => _state;
    public Node CameFrom => _cameFrom;

    public Direction Move => _move;

    public Node(State state, Node cameFrom, Direction move)
    {
        _state = state;
        _cameFrom = cameFrom;
        _move = move;
    }

    public Node NodeFromMove(Node parent, Direction direction)
    {
        return new Node(parent.State.StateFromMove(direction), parent, direction);
    }
}
