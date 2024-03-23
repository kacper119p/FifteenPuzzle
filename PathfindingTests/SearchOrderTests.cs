using Pathfinding;

namespace PathfindingTests;

public class SearchOrderTests
{
    [Test]
    public void ConstructorTest()
    {
        Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        Assert.DoesNotThrow(() => new SearchOrder(directions));

        directions = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.Right };
        Assert.Throws<ArgumentException>(() => new SearchOrder(directions));

        directions = new Direction[] { Direction.Up, Direction.Left, Direction.Right };
        Assert.Throws<ArgumentException>(() => new SearchOrder(directions));
    }

    [Test]
    public void EnumerationTest()
    {
        Direction[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        SearchOrder searchOrder = new SearchOrder(directions);
        int i = 0;
        foreach (Direction direction in searchOrder)
        {
            Assert.AreEqual(searchOrder[i], direction);
            i++;
        }
    }
}
