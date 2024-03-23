using Pathfinding;

namespace Program;

public static class OutputUtility
{
    public static char DirectionToChar(Direction direction)
    {
        return direction switch
        {
            Direction.Up => 'U',
            Direction.Down => 'D',
            Direction.Left => 'L',
            Direction.Right => 'R',
            _ => throw new ArgumentException("Invalid Direction")
        };
    }
}
