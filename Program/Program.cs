using Pathfinding;
using Pathfinding.Exceptions;

namespace Program;

public static class Program
{
    public static int Main(string[] args)
    {
        string argStrategy = args[0];
        string argSearchOrderHeuristic = args[1];
        string argInputFile = args[2];
        string argSolutionFile = args[3];
        string argStatsFile = args[4];

        Strategy strategy;
        State startState;
        try
        {
            strategy = Parser.ParseStrategy(argStrategy);
            startState = Parser.ParseInputFile(argInputFile);
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
                Strategy.Bfs => new BfsSolver(Parser.ParseSearchOrder(argSearchOrderHeuristic)),
                Strategy.Dfs => new DfsSolver(Parser.ParseSearchOrder(argSearchOrderHeuristic)),
                Strategy.AStar => new AStarSolver(Parser.ParseHeuristic(argSearchOrderHeuristic)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (Parser.ParserException e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }

        PathfindingData solutionData;
        try
        {
            solutionData = solver.Solve(startState, goal);
        }
        catch (SolutionNotFoundException e)
        {
            OutputUtility.OutputError(argSolutionFile);
            OutputUtility.OutputError(argStatsFile);
            return 0;
        }


        OutputUtility.OutputSolution(argSolutionFile, solutionData);
        OutputUtility.OutputStats(argStatsFile, solutionData);

        return 0;
    }
}
