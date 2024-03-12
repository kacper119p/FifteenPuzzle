using Pathfinding;

namespace PathfindingTests;

public class StateTests
{
    [Test]
    public void ValidationTest()
    {
        ushort[,] fields = { { 1, 2 }, { 3, 0 } };

        State state = new State(fields);
        Assert.True(state.Validate());

        fields[1, 0] = 0;
        state = new State(fields);
        Assert.False(state.Validate());

        fields[1, 0] = 3;
        state = new State(fields);
        Assert.True(state.Validate());

        fields[1, 0] = 10;
        state = new State(fields);
        Assert.False(state.Validate());

        fields[1, 1] = 1;
        Assert.Throws<ArgumentException>(() => state = new State(fields));
    }

    [Test]
    public void MoveTest()
    {
        ushort[,] fields = { { 1, 2 }, { 3, 0 } };
        State state = new State(fields);
        state = state.StateFromMove(Direction.Left);

        ushort[,] target = { { 1, 2 }, { 0, 3 } };
        CompareStateAndArray(state, target);
        state = state.StateFromMove(Direction.Up);

        target = new ushort[,] { { 0, 2 }, { 1, 3 } };
        CompareStateAndArray(state, target);

        Assert.Throws<ArgumentException>(() => state.StateFromMove(Direction.Left));
        Assert.Throws<ArgumentException>(() => state.StateFromMove(Direction.Up));

        state = state.StateFromMove(Direction.Right);

        target = new ushort[,] { { 2, 0 }, { 1, 3 } };
        CompareStateAndArray(state, target);

        state = state.StateFromMove(Direction.Down);

        target = new ushort[,] { { 2, 3 }, { 1, 0 } };
        CompareStateAndArray(state, target);

        Assert.Throws<ArgumentException>(() => state.StateFromMove(Direction.Down));
        Assert.Throws<ArgumentException>(() => state.StateFromMove(Direction.Right));
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
        ushort[,] fields1 = { { 1, 2 }, { 3, 0 } };
        ushort[,] fields2 = { { 1, 2 }, { 3, 0 } };
        State state1 = new State(fields1);
        State state2 = new State(fields2);

        Assert.True(state1 == state2);
        Assert.True(state1.Equals(state2));
        Assert.False(state1 != state2);
        Assert.AreEqual(state1.GetHashCode(), state2.GetHashCode());

        fields2 = new ushort[,] { { 0, 2 }, { 1, 3 } };
        state2 = new State(fields2);

        Assert.False(state1 == state2);
        Assert.False(state1.Equals(state2));
        Assert.True(state1 != state2);

        Assert.False(state1.Equals(new bool()));

        fields2 = new ushort[,] { { 3, 0 } };
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
}
