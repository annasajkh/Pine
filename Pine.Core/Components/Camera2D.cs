using DotNext;
using Foster.Framework;
using System.Numerics;

namespace Pine.Core.Components;

public enum Camera2DErrorCode
{
    Success = 0,
    CannotConvert,
}

public sealed class Camera2D
{
    /// <summary>
    /// The position of the camera.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// The size of the camera
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// The rotation of the camera.
    /// </summary>
    public float Rotation { get; set; }

    /// <summary> 
    /// The origin of the camera.
    /// </summary>
    public Vector2 Origin
    {
        get
        {
            return new Vector2(Size.X / 2, Size.Y / 2);
        }
    }

    /// <summary>
    /// The camera zoom.
    /// </summary>
    public float Zoom { get; set; }

    /// <summary>
    /// Camera rectangle boundary in the world space.
    /// </summary>
    public Rect BoundingBox
    {
        get
        {
            return new Rect(
                x: Position.X - Origin.X / Zoom,
                y: Position.Y - Origin.Y / Zoom,
                w: Size.X / Zoom,
                h: Size.Y / Zoom
            );
        }
    }

    /// <summary>
    /// The view matrix of the camera, use this on the batcher.PushMatrix() method to transform everything into the camera view.
    /// </summary>
    public Matrix3x2 ViewMatrix
    {
        get
        {
            return Matrix3x2.CreateTranslation(-(Position - Origin)) *
                   Matrix3x2.CreateTranslation(-Origin) *
                   Matrix3x2.CreateRotation(Rotation) *
                   Matrix3x2.CreateScale(Zoom, Zoom) *
                   Matrix3x2.CreateTranslation(Origin);
        }
    }

    /// <summary>
    ///  Project world space of this camera to screen space.
    /// </summary>
    /// <param name="worldPosition">The world space position.</param>
    public Vector2 ToScreenSpace(Vector2 worldPosition)
    {
        return Vector2.Transform(worldPosition, ViewMatrix);
    }

    /// <summary>
    ///  Project screen space to world space of this camera.
    /// </summary>
    /// <param name="spacePosition">The screen space position.</param>
    public Result<Vector2, Camera2DErrorCode> ToWorldSpace(Vector2 spacePosition)
    {
        Matrix3x2 invertedMatrix;

        if (!Matrix3x2.Invert(ViewMatrix, out invertedMatrix))
        {
            return new Result<Vector2, Camera2DErrorCode>(Camera2DErrorCode.CannotConvert);
        }
        
        return Vector2.Transform(spacePosition, invertedMatrix);
    }

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="position">The position of the camera.</param>
    /// <param name="rotation">The rotation of the camera.</param>
    /// <param name="zoom">The camera zoom</param>
    public Camera2D(Vector2 position, Vector2 size, float rotation, float zoom)
    {
        Position = position;
        Size = size;
        Rotation = rotation;
        Zoom = zoom;
    }
}
