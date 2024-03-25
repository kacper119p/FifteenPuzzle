using System.Diagnostics;
using Pathfinding.Exceptions;

namespace Pathfinding;

public class DfsSolver : ISolver
{
    private const int MaxAllowedDepth = 20;
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

        if (start == goal)
        {
            return new PathfindingData()
            {
                solution = new LinkedList<Direction>(),
                solutionLength = 0,
                statesVisited = 1,
                statesProcessed = 0,
                maxDepth = maxDepth,
                processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
            };
        }

        Stack<Node> open = new Stack<Node>();
        HashSet<Node> visited = new HashSet<Node>();
        Dictionary<Node, int> processed = new Dictionary<Node, int>();

        Node startNode = new Node(start);

        open.Push(startNode);
        visited.Add(startNode);
        while (open.Count > 0)
        {
            Node current = open.Pop();

            try
            {
                processed.Add(current, current.Depth);
            }
            catch (ArgumentException)
            {
                processed[current] = current.Depth;
            }
            
            if (current.State == goal)
            {
                LinkedList<Direction> solution = current.TraceBack();
                return new PathfindingData()
                {
                    solution = solution,
                    solutionLength = solution.Count,
                    statesVisited = visited.Count,
                    statesProcessed = processed.Count,
                    maxDepth = maxDepth,
                    processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
                };
            }
            
            if (current.Depth >= MaxAllowedDepth)
            {
                continue;
            }
            
            for (int i = SearchOrder.DirectionsCount - 1; i >= 0; i--)
            {
                try
                {
                    Node neighbour = current.NodeFromMove(_searchOrder[i]);
                    visited.Add(neighbour);

                    if (neighbour.Depth > maxDepth)
                    {
                        maxDepth = neighbour.Depth;
                    }
                    
                    if (!processed.ContainsKey(neighbour) || processed[neighbour] >= neighbour.Depth)
                    {
                        open.Push(neighbour);
                    }
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
            statesProcessed = processed.Count,
            maxDepth = maxDepth,
            processingTimeMilliseconds = stopwatch.Elapsed.TotalMilliseconds
        });
    }
}
