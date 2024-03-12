namespace UtilityLibrary;

public static class VariablesUtility
{
    public static void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }
}
