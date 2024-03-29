using Pathfinding;
using Pathfinding.Heuristics;

namespace PathfindingTests;

public class DfsTests
{
    public void SolvingTestBasic()
    {
        SearchOrder searchOrder = new SearchOrder(new Direction[]
            { Direction.Right, Direction.Down, Direction.Left, Direction.Up });
        ISolver solver = new DfsSolver(searchOrder, SolverTestsGeneric.RandomTestDepth);
        SolverTestsGeneric.SolvingTestBasic(solver);
    }

    [Test]
    public void SolvingTestRandom()
    {
        SearchOrder searchOrder = new SearchOrder(new Direction[]
            { Direction.Right, Direction.Down, Direction.Left, Direction.Up });
        ISolver solver = new DfsSolver(searchOrder, SolverTestsGeneric.RandomTestDepth);
        SolverTestsGeneric.SolvingTestRandom(solver);
    }
}
