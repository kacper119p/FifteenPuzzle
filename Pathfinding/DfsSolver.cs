using System.Diagnostics;
using Pathfinding.Exceptions;

namespace Pathfinding;

public class DfsSolver : ISolver
{
    private readonly SearchOrder _searchOrder;

    public DfsSolver(SearchOrder searchOrder)
    {
        _searchOrder = searchOrder;
    }

    public PathfindingData Solve(State start, State goal)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int maxDepth = 0;
        long statesVisited = 0;
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

        Stack<Node> open = new Stack<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        Node startNode = new Node(start, null, Direction.None);

        open.Push(startNode);
        visited.Add(startNode);
        while (open.Count > 0)
        {
            Node current = open.Pop();

            statesProcessed++;
            for (int i = SearchOrder.DirectionsCount - 1; i >= 0; i--)
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

                    statesVisited++;
                    if (neighbour.State == goal)
                    {
                        LinkedList<Direction> solution = neighbour.TraceBack();
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

                    open.Push(neighbour);
                }
                catch (MoveException)
                {
                }
            }
        }

        throw new SolutionNotFoundException();
    }
}
