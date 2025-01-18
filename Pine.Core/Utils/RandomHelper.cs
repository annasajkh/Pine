namespace Pine.Core.Utils;

public static class RandomHelper
{
    static Random random = new();

    public static float NextFloat()
    {
        return random.NextSingle();
    }
}
