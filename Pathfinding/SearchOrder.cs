using System.Collections;

namespace Pathfinding;

public class SearchOrder : IEnumerable<Direction>
{
    private const int DirectionsCount = 4;
    private readonly Direction[] _directions = new Direction[DirectionsCount];
    public Direction this[int index] => _directions[index];

    public SearchOrder(Direction[] directions)
    {
        if (directions.Length != DirectionsCount)
        {
            throw new ArgumentException("Invalid directions count");
        }

        for (int i = 0; i < DirectionsCount; i++)
        {
            _directions[i] = directions[i];
        }
    }

    public IEnumerator<Direction> GetEnumerator()
    {
        return new SearchOrderEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class SearchOrderEnumerator : IEnumerator<Direction>
    {
        private int _current = -1;
        private SearchOrder _searchOrder;

        public SearchOrderEnumerator(SearchOrder searchOrder)
        {
            _searchOrder = searchOrder;
        }
        
        public bool MoveNext()
        {
            _current++;
            return _current < DirectionsCount;
        }

        public void Reset()
        {
            _current = -1;
        }

        public Direction Current { get => _searchOrder._directions[_current]; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
