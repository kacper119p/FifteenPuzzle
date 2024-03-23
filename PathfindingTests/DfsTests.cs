using Pathfinding;

namespace PathfindingTests;

public class DfsTests
{
    [Test]
    public void SolvingTest()
    {
        State start = new State(new byte[,] { { 0, 1 }, { 3, 2 } });
        State goal = State.GenerateSolved(2, 2);
        SearchOrder searchOrder = new SearchOrder(new Direction[]
            { Direction.Right, Direction.Down, Direction.Left, Direction.Up });

        PathfindingData result = Dfs.Solve(start, goal, searchOrder);
        State current = start;
        foreach (Direction move in result.solution)
        {
            current = current.StateFromMove(move);
        }
        
        Assert.AreEqual(goal, current);
    }
}
