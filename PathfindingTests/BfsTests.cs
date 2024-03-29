using Pathfinding;
using Pathfinding.Heuristics;

namespace PathfindingTests;

public class BfsTests
{
    [Test]
    public void SolvingTestBasic()
    {
        SearchOrder searchOrder = new SearchOrder(new Direction[]
            { Direction.Right, Direction.Down, Direction.Left, Direction.Up });
        ISolver solver = new BfsSolver(searchOrder);
        SolverTestsGeneric.SolvingTestBasic(solver);
    }

    [Test]
    public void SolvingTestRandom()
    {
        SearchOrder searchOrder = new SearchOrder(new Direction[]
            { Direction.Right, Direction.Down, Direction.Left, Direction.Up });
        ISolver solver = new BfsSolver(searchOrder);
        SolverTestsGeneric.SolvingTestRandom(solver);
    }
}
