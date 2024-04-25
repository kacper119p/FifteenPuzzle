using Pathfinding;
using Pathfinding.Heuristics;

namespace PathfindingTests;

public class AStarTests
{
    [Test]
    public void SolvingTestBasic()
    {
        ISolver solver = new AStarSolver(new Manhattan());
        SolverTestsGeneric.SolvingTestBasic(solver);
    }

    [Test]
    public void SolvingTestSolved()
    {
        ISolver solver = new AStarSolver(new Hamming());
        SolverTestsGeneric.SolvingTestSolved(solver);
    }

    [Test]
    public void SolvingTestRandom()
    {
        ISolver solver = new AStarSolver(new Manhattan());
        SolverTestsGeneric.SolvingTestRandom(solver);
    }
}
