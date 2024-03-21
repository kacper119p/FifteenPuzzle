namespace Pathfinding.Exceptions;

public class MoveException : Exception
{
    public MoveException(){}
    public MoveException(string message) : base(message){}
}
