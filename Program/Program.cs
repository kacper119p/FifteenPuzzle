using Pathfinding;
using Pathfinding.Heuristics;

namespace Program;

public static class Program
{
    public static int Main(string[] args)
    {
        /*
         * args
         * 0 - strategy
         * 1 - searchOrder/heuristic
         * 2 - input file
         * 3 - solution file
         * 4 - stats file
         */

        Strategy strategy;
        State startState;
        try
        {
            strategy = Parser.ParseStrategy(args[0]);
            startState = Parser.ParseInputFile(args[2]);
        }
        catch (Parser.ParserException e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }

        State goal = State.GenerateSolved(startState.Height, startState.Width);
        ISolver solver;
        try
        {
            solver = strategy switch
            {
                Strategy.Bfs => new BfsSolver(Parser.ParseSearchOrder(args[1])),
                Strategy.Dfs => new DfsSolver(Parser.ParseSearchOrder(args[1])),
                Strategy.AStar => new AStarSolver(Parser.ParseHeuristic(args[1])),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Parser.ParserException e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }

        PathfindingData solutionData = solver.Solve(startState, goal);

        OutputUtility.OutputSolution(args[3], solutionData);
        OutputUtility.OutputStats(args[4], solutionData);

        return 0;
    }
}
