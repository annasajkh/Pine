using System.Numerics;

namespace Foster.Extended.Extensions;

public static class Vector2Ext
{
    public static Vector2 RotateRadians(this Vector2 value, float radians)
    {
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        return new Vector2(cos * value.X - sin * value.Y, sin * value.X + cos * value.Y);
    }
}
