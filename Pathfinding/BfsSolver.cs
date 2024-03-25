using System.Diagnostics;
using Pathfinding.Exceptions;

namespace Pathfinding;

public class BfsSolver : ISolver
{
    private readonly SearchOrder _searchOrder;

    public BfsSolver(SearchOrder searchOrder)
    {
        _searchOrder = searchOrder;
    }

    public PathfindingData Solve(State start, State goal)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int maxDepth = 0;
        long statesProcessed = 0;

        if (start == goal)
        {
            return new PathfindingData()
            {
                solution = new LinkedList<Direction>(),
                solutionLength = 0,
                statesVisited = 1,
                statesProcessed = statesProcessed,
                maxDepth = maxDepth,
                processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
            };
        }

        Queue<Node> open = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        Node startNode = new Node(start);

        open.Enqueue(startNode);
        visited.Add(startNode);
        while (open.Count > 0)
        {
            Node current = open.Dequeue();

            statesProcessed++;

            for (int i = 0; i < SearchOrder.DirectionsCount; i++)
            {
                try
                {
                    Node neighbour = current.NodeFromMove(_searchOrder[i]);
                    if (!visited.Add(neighbour))
                    {
                        continue;
                    }

                    if (neighbour.Depth > maxDepth)
                    {
                        maxDepth = neighbour.Depth;
                    }

                    if (neighbour.State == goal)
                    {
                        LinkedList<Direction> solution = neighbour.TraceBack();
                        return new PathfindingData()
                        {
                            solution = solution,
                            solutionLength = solution.Count,
                            statesVisited = visited.Count,
                            statesProcessed = statesProcessed,
                            maxDepth = maxDepth,
                            processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
                        };
                    }

                    open.Enqueue(neighbour);
                }
                catch (MoveException)
                {
                }
            }
        }

        throw new SolutionNotFoundException(new PathfindingData()
        {
            solution = null,
            solutionLength = -1,
            statesVisited = visited.Count,
            statesProcessed = statesProcessed,
            maxDepth = maxDepth,
            processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
        });
    }
}
