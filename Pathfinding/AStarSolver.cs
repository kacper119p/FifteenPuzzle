using System.Diagnostics;
using Pathfinding.Exceptions;
using Pathfinding.Heuristics;

namespace Pathfinding;

public class AStarSolver : ISolver
{
    private readonly IHeuristic _heuristic;

    private static readonly SearchOrder SearchOrder
        = new SearchOrder(new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right });

    public AStarSolver(IHeuristic heuristic)
    {
        _heuristic = heuristic;
    }

    public PathfindingData Solve(State start, State goal)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int maxDepth = 0;
        long statesVisited = 1;
        long statesProcessed = 0;

        if (start == goal)
        {
            return new PathfindingData()
            {
                solution = new LinkedList<Direction>(),
                solutionLength = 0,
                statesVisited = statesVisited,
                statesProcessed = statesProcessed,
                maxDepth = maxDepth,
                processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
            };
        }

        PriorityQueue<AStarNode, ulong> open = new PriorityQueue<AStarNode, ulong>();
        HashSet<AStarNode> visited = new HashSet<AStarNode>();
        HashSet<AStarNode> processed = new HashSet<AStarNode>();

        AStarNode startNode = new AStarNode(start, null, Direction.None);

        open.Enqueue(startNode, 0ul);
        
        while (open.Count > 0)
        {
            AStarNode current = open.Dequeue();

            if (!processed.Add(current))
            {
                continue;
            }
            
            statesProcessed++;

            if (current.State == goal)
            {
                LinkedList<Direction> solution = current.TraceBack();
                return new PathfindingData()
                {
                    solution = solution,
                    solutionLength = solution.Count,
                    statesVisited = statesVisited,
                    statesProcessed = statesProcessed,
                    maxDepth = maxDepth,
                    processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
                };
            }

            for (int i = 0; i < SearchOrder.DirectionsCount; i++)
            {
                try
                {
                    AStarNode neighbour = current.NodeFromMove(SearchOrder[i], _heuristic, goal);
                    if (processed.Contains(neighbour))
                    {
                        continue;
                    }

                    if (visited.Add(neighbour))
                    {
                        statesVisited++;
                    }
                    
                    if (neighbour.Depth > maxDepth)
                    {
                        maxDepth = neighbour.Depth;
                    }

                    open.Enqueue(neighbour, neighbour.Cost);
                }
                catch (MoveException)
                {
                }
            }
        }

        throw new SolutionNotFoundException();
    }
}
