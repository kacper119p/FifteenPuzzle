using System.Runtime.InteropServices;

namespace Pathfinding;

internal class Node
{
    private readonly State _state;
    private readonly Node? _cameFrom;
    private readonly Direction _move;
    private readonly int _depth;

    public State State => _state;
    public Node? CameFrom => _cameFrom;

    public Direction Move => _move;

    public int Depth => _depth;

    public Node(State state, Node? cameFrom, Direction move)
    {
        _state = state;
        _cameFrom = cameFrom;
        _depth = _cameFrom != null ? _cameFrom._depth + 1 : 0;
        _move = move;
    }

    public Node NodeFromMove(Direction direction)
    {
        return new Node(this.State.StateFromMove(direction), this, direction);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Node node)
        {
            return true;
        }
        return _state.Equals(node._state);
    }

    public static bool operator ==(Node? a, Node? b)
    {
        if (a is not null) return a._state.Equals(b?._state);
        return b is null;
    }

    public static bool operator !=(Node? a, Node? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return _state.GetHashCode();
    }

    public LinkedList<Direction> TraceBack()
    {
        LinkedList<Direction> result = new LinkedList<Direction>();
        Node? current = this;
        do
        {
            result.AddFirst(current.Move);
            current = current.CameFrom;
        } while (current!.Move != Direction.None);

        return result;
    }
}
