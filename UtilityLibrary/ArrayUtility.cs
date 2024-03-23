namespace UtilityLibrary;

public static class ArrayUtility
{
    public static T[,] Copy2DArray<T>(this T[,] original)
    {
        T[,] result = new T[original.GetLength(0), original.GetLength(1)];

        for (int x = 0; x < original.GetLength(0); x++)
        {
            for (int y = 0; y < original.GetLength(1); y++)
            {
                result[x, y] = original[x, y];
            }
        }

        return result;
    }
}
