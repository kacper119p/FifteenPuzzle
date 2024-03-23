namespace Pathfinding;

internal struct Point<T>
{
    public readonly T x;
    public readonly T y;

    public Point(T x, T y)
    {
        this.x = x;
        this.y = y;
    }
}
