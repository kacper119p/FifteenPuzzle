namespace Pathfinding.Exceptions;

public class SolutionNotFoundException : Exception
{
    public SolutionNotFoundException(string message) : base(message)
    {
    }

    public SolutionNotFoundException()
    {
    }
}
