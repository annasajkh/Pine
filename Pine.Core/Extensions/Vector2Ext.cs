using System.Numerics;
using Pine.Core.Utils;

namespace Pine.Core.Extensions;

public static class Vector2Ext
{
    /// <summary>
    /// Rotate vector2 by a radians value.
    /// </summary>
    /// <param name="radians">The radians value.</param>
    /// <returns>The rotated vector2.</returns>
    public static Vector2 RotateRadians(this Vector2 value, float radians)
    {
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        return new Vector2(cos * value.X - sin * value.Y, sin * value.X + cos * value.Y);
    }

    /// <summary>
    /// Rotate vector2 by a degrees value.
    /// </summary>
    /// <param name="degrees">The degrees value.</param>
    /// <returns>The rotated vector2.</returns>
    public static Vector2 RotateDegrees(this Vector2 value, float degrees)
    {
        float radians = MathHelper.DegreesToRadians(degrees);

        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        return new Vector2(cos * value.X - sin * value.Y, sin * value.X + cos * value.Y);
    }
}
