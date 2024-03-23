using Pathfinding.Exceptions;
using UtilityLibrary;

namespace Pathfinding;

public class State : IEquatable<State>
{
    private readonly byte[,] _fields;
    private readonly Point<int> _zeroPosition;

    /// <summary>
    /// Height of the board.
    /// </summary>
    public int Height => _fields.GetLength(0);

    /// <summary>
    /// Width of the board
    /// </summary>
    public int Width => _fields.GetLength(1);

    /// <summary>
    /// Returns field value at given position.
    /// </summary>
    public ushort this[int x, int y] => _fields[x, y];

    /// <summary>
    /// Creates new 15puzzle board from 2d array of bytes.<br/>
    /// Supports up to 15x15 boards.
    /// </summary>
    /// <param name="fields">Fields for 15puzzle board.</param>
    ///<exception cref="ArgumentException">Thrown when given fields aren't valid for 15puzzle board.</exception>
    public State(byte[,] fields)
    {
        _fields = new byte[fields.GetLength(0), fields.GetLength(1)];
        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                _fields[x, y] = fields[x, y];
            }
        }

        _zeroPosition = GetZeroPosition();
        if (!Validate())
        {
            throw new ArgumentException("invalid fields");
        }
    }

    private State(byte[,] fields, Point<int> zeroPosition)
    {
        _fields = fields;
        _zeroPosition = zeroPosition;
    }

    public bool Equals(State? other)
    {
        if (other == null)
        {
            return false;
        }

        if (this.Height != other.Height || this.Width != other.Width)
        {
            return false;
        }

        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                if (this[x, y] != other[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as State);
    }

    public static bool operator ==(State? a, State? b)
    {
        if (a is not null) return a.Equals(b);
        return b is null;
    }

    public static bool operator !=(State? a, State? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new HashCode();
        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                hashCode.Add(this[x, y]);
            }
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Attempts to create new State by moving empty field accordingly.
    /// </summary>
    /// <param name="direction">Direction to move.</param>
    /// <returns></returns>
    /// <exception cref="MoveException">Move is impossible.</exception>
    /// <exception cref="ArgumentException">Given Direction is invalid.</exception>
    public State StateFromMove(Direction direction)
    {
        byte[,] fields;
        Point<int> zeroPosition;
        switch (direction)
        {
            case Direction.Up:
                if (_zeroPosition.x <= 0)
                {
                    throw new MoveException("IllegalMove");
                }

                fields = _fields.Copy2DArray();
                VariablesUtility.Swap(ref fields[_zeroPosition.x, _zeroPosition.y],
                    ref fields[_zeroPosition.x - 1, _zeroPosition.y]);
                zeroPosition = new Point<int>(_zeroPosition.x - 1, _zeroPosition.y);
                break;
            case Direction.Down:
                if (_zeroPosition.x >= _fields.GetUpperBound(0))
                {
                    throw new MoveException("IllegalMove");
                }

                fields = _fields.Copy2DArray();
                VariablesUtility.Swap(ref fields[_zeroPosition.x, _zeroPosition.y],
                    ref fields[_zeroPosition.x + 1, _zeroPosition.y]);
                zeroPosition = new Point<int>(_zeroPosition.x + 1, _zeroPosition.y);
                break;
            case Direction.Left:
                if (_zeroPosition.y <= 0)
                {
                    throw new MoveException("IllegalMove");
                }

                fields = _fields.Copy2DArray();
                VariablesUtility.Swap(ref fields[_zeroPosition.x, _zeroPosition.y],
                    ref fields[_zeroPosition.x, _zeroPosition.y - 1]);
                zeroPosition = new Point<int>(_zeroPosition.x, _zeroPosition.y - 1);
                break;
            case Direction.Right:
                if (_zeroPosition.y >= _fields.GetUpperBound(1))
                {
                    throw new MoveException("IllegalMove");
                }

                fields = _fields.Copy2DArray();
                VariablesUtility.Swap(ref fields[_zeroPosition.x, _zeroPosition.y],
                    ref fields[_zeroPosition.x, _zeroPosition.y + 1]);
                zeroPosition = new Point<int>(_zeroPosition.x, _zeroPosition.y + 1);
                break;
            default:
                throw new ArgumentException("Bad Direction");
        }

        return new State(fields, zeroPosition);
    }

    /// <summary>
    /// Generates solved 15puzzle board.
    /// </summary>
    /// <param name="height">Desired height of the board.</param>
    /// <param name="width">Desired width of the board</param>
    /// <returns>Solved 15puzzle board.</returns>
    /// <exception cref="ArgumentException">Height or Width is 0, or Height * Width is greater than 255.</exception>
    public static State GenerateSolved(int height, int width)
    {
        if (height == 0 || width == 0)
        {
            throw new ArgumentException("Dimension can't be 0");
        }

        if (height * width > byte.MaxValue)
        {
            throw new ArgumentException("Board too big");
        }

        byte[,] fields = new byte[height, width];
        byte k = 1;
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                fields[x, y] = k;
                k++;
            }
        }

        Point<int> zeroPosition = new Point<int>(fields.GetUpperBound(0), fields.GetUpperBound(1));
        fields[zeroPosition.x, zeroPosition.y] = 0;
        return new State(fields, zeroPosition);
    }

    private Point<int> GetZeroPosition()
    {
        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                if (this[x, y] == 0)
                {
                    return new Point<int>(x, y);
                }
            }
        }

        throw new ArgumentException("No zero in fields");
    }

    private bool Validate()
    {
        int max = Height * Width;
        if (max > byte.MaxValue)
        {
            return false;
        }

        for (byte i = 0; i < max; i++)
        {
            int count = 0;
            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (this[x, y] == i)
                    {
                        count++;
                        if (count > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            if (count != 1)
            {
                return false;
            }
        }

        return true;
    }
}
