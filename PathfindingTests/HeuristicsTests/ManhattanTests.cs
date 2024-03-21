using Pathfinding;
using Pathfinding.Heuristics;

namespace PathfindingTests.HeuristicsTests;

public class ManhattanTests
{
    [Test]
    public void EvaluationTest()
    {
        ushort[,] fields1 = { { 1, 2 }, { 3, 0 } };
        State state1 = new State(fields1);
        State state2 = new State(fields1);
        IHeuristic manhattan = new Manhattan();
        Assert.AreEqual(0, manhattan.Evaluate(state1, state2));
        
        ushort[,] fields2 = { { 2, 1 }, { 0, 3 } };
        state2 = new State(fields2);
        Assert.AreEqual(3, manhattan.Evaluate(state1, state2));
        
        ushort[,] fields3 = { { 0, 2 }, { 3, 1 } };
        state2 = new State(fields3);
        Assert.AreEqual(2, manhattan.Evaluate(state1, state2));
        
        ushort[,] fields4 = { { 1, 0 }};
        state2 = new State(fields4);
        Assert.Throws<ArgumentException>(() => manhattan.Evaluate(state1, state2));
    }
}
