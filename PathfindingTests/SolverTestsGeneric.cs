using System.Collections;
using Pathfinding;
using Pathfinding.Exceptions;

namespace PathfindingTests;

public static class SolverTestsGeneric
{
    public const int RandomTestDepth = 7;

    public static void SolvingTestBasic(ISolver solver)
    {
        State start = new State(new byte[,] { { 0, 1 }, { 3, 2 } });
        State goal = State.GenerateSolved(2, 2);
        PathfindingData result = solver.Solve(start, goal);
        State current = start;
        foreach (Direction move in result.solution)
        {
            current = current.StateFromMove(move);
        }

        Assert.AreEqual(goal, current);
    }

    public static void SolvingTestRandom(ISolver solver)
    {
        Direction[] directions = { Direction.Down, Direction.Left, Direction.Up, Direction.Right };
        State goal = State.GenerateSolved(4, 4);
        State start = State.GenerateSolved(4, 4);
        Random random = new Random();
        for (int i = 0; i < RandomTestDepth;)
        {
            try
            {
                start = start.StateFromMove(directions[random.Next(0, directions.Length)]);
                i++;
            }
            catch (MoveException)
            {
            }
        }

        PathfindingData result = solver.Solve(start, goal);
        State current = start;
        Assert.NotNull(result.solution);
        foreach (Direction move in result.solution!)
        {
            current = current.StateFromMove(move);
        }

        Assert.AreEqual(goal, current);
    }
}
