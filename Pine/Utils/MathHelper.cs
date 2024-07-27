using Foster.Extended.DataStructures;
using System.Numerics;

namespace Foster.Extended.Utils;

public static class MathHelper
{
    /// <summary>
    /// Convert degrees to radians
    /// </summary>
    /// <param name="degrees">The value in degrees</param>
    /// <returns></returns>
    public static float DegreesToRadians(float degrees)
    {
        return degrees * (MathF.PI / 180);
    }

    /// <summary>
    /// Convert radians to degrees
    /// </summary>
    /// <param name="radians">The value in radians</param>
    /// <returns></returns>
    public static float RadiansToDegrees(float radians)
    {
        return radians * (180 / MathF.PI);
    }

    /// <summary>
    /// Snap a value into a grid a grid
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="gridSize">The size of the grid</param>
    /// <returns>The snapped value</returns>
    public static float SnapToGrid(float value, float gridSize)
    {
        return (float)(MathF.Floor(value / gridSize) * gridSize);
    }

    /// <summary>
    /// Snap a Vector2 into a grid a grid
    /// </summary>
    /// <param name="value">The Vector2</param>
    /// <param name="gridSize">The size of the grid</param>
    /// <returns>The snapped Vector2</returns>
    public static Vector2 SnapToGrid(Vector2 value, int gridSize)
    {
        return new Vector2(SnapToGrid(value.X, gridSize), SnapToGrid(value.Y, gridSize));
    }

    /// <summary>
    /// Snap a Vector2i into a grid a grid
    /// </summary>
    /// <param name="value">The Vector2i</param>
    /// <param name="gridSize">The size of the grid</param>
    /// <returns>The snapped Vector2i</returns>
    public static Vector2i SnapToGrid(Vector2i value, int gridSize)
    {
        return new Vector2i((int)SnapToGrid(value.X, gridSize), (int)SnapToGrid(value.Y, gridSize));
    }
}