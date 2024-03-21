using Pathfinding;
using Pathfinding.Heuristics;

namespace PathfindingTests.HeuristicsTests;

public class HammingTests
{
    [Test]
    public void EvaluationTest()
    {
        ushort[,] fields1 = { { 1, 2 }, { 3, 0 } };
        State state1 = new State(fields1);
        State state2 = new State(fields1);
        IHeuristic hamming = new Hamming();
        Assert.AreEqual(0, hamming.Evaluate(state1, state2));
        
        ushort[,] fields2 = { { 2, 1 }, { 0, 3 } };
        state2 = new State(fields2);
        Assert.AreEqual(3, hamming.Evaluate(state1, state2));
        
        ushort[,] fields3 = { { 1, 0 }};
        state2 = new State(fields3);
        Assert.Throws<ArgumentException>(() => hamming.Evaluate(state1, state2));
    }
}
