using Pathfinding;
using Pathfinding.Exceptions;

namespace PathfindingTests;

public class StateTests
{
    [Test]
    public void ValidationTest()
    {
        byte[,] fields = { { 1, 2 }, { 3, 0 } };
        Assert.DoesNotThrow(() => new State(fields));

        fields[1, 0] = 0;
        Assert.Throws<ArgumentException>(() => new State(fields));

        fields[1, 0] = 3;
        Assert.DoesNotThrow(() => new State(fields));

        fields[1, 0] = 10;
        Assert.Throws<ArgumentException>(() => new State(fields));

        fields[1, 1] = 1;
        Assert.Throws<ArgumentException>(() => new State(fields));
    }

    [Test]
    public void MoveTest()
    {
        byte[,] fields = { { 1, 2 }, { 3, 0 } };
        State state = new State(fields);
        state = state.StateFromMove(Direction.Left);

        ushort[,] target = { { 1, 2 }, { 0, 3 } };
        CompareStateAndArray(state, target);
        state = state.StateFromMove(Direction.Up);

        target = new ushort[,] { { 0, 2 }, { 1, 3 } };
        CompareStateAndArray(state, target);

        Assert.Throws<MoveException>(() => state.StateFromMove(Direction.Left));
        Assert.Throws<MoveException>(() => state.StateFromMove(Direction.Up));

        state = state.StateFromMove(Direction.Right);

        target = new ushort[,] { { 2, 0 }, { 1, 3 } };
        CompareStateAndArray(state, target);

        state = state.StateFromMove(Direction.Down);

        target = new ushort[,] { { 2, 3 }, { 1, 0 } };
        CompareStateAndArray(state, target);

        Assert.Throws<MoveException>(() => state.StateFromMove(Direction.Down));
        Assert.Throws<MoveException>(() => state.StateFromMove(Direction.Right));
        Assert.Throws<ArgumentException>(() => state.StateFromMove(Direction.None));
    }

    private void CompareStateAndArray(State state, ushort[,] array)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                Assert.AreEqual(array[x, y], state[x, y]);
            }
        }
    }

    [Test]
    public void EqualityTest()
    {
        byte[,] fields1 = { { 1, 2 }, { 3, 0 } };
        byte[,] fields2 = { { 1, 2 }, { 3, 0 } };
        State state1 = new State(fields1);
        State state2 = new State(fields2);

        Assert.True(state1 == state2);
        Assert.True(state1.Equals(state2));
        Assert.False(state1 != state2);
        Assert.AreEqual(state1.GetHashCode(), state2.GetHashCode());

        fields2 = new byte[,] { { 0, 2 }, { 1, 3 } };
        state2 = new State(fields2);

        Assert.False(state1 == state2);
        Assert.False(state1.Equals(state2));
        Assert.True(state1 != state2);

        Assert.False(state1.Equals(new bool()));

        fields2 = new byte[,] { { 1, 0 } };
        state2 = new State(fields2);
        Assert.False(state1 == state2);
        Assert.False(state1.Equals(state2));
        Assert.True(state1 != state2);

        state2 = null;
        Assert.False(state1 == state2);
        Assert.False(state1.Equals(state2));
        Assert.True(state1 != state2);

        state1 = null;
        Assert.True(state1 == state2);
        Assert.False(state1 != state2);
    }

    [Test]
    public void SolvedTest()
    {
        State solved = State.GenerateSolved(2, 2);
        Assert.AreEqual(1, solved[0, 0]);
        Assert.AreEqual(2, solved[0, 1]);
        Assert.AreEqual(3, solved[1, 0]);
        Assert.AreEqual(0, solved[1, 1]);
        Assert.Throws<ArgumentException>(() => State.GenerateSolved(16, 16));
        Assert.Throws<ArgumentException>(() => State.GenerateSolved(300, 1));
        Assert.Throws<ArgumentException>(() => State.GenerateSolved(1, 300));
        Assert.Throws<ArgumentException>(() => State.GenerateSolved(0, 1));
        Assert.Throws<ArgumentException>(() => State.GenerateSolved(1, 0));
    }
}
