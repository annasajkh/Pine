using Foster.Framework;
using System.Numerics;

namespace Foster.Extended.Components;

public sealed class Camera2D
{
    /// <summary>
    /// The position of the camera
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// The rotation of the camera
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// The origin of the camera
    /// </summary>
    public Vector2 Origin
    {
        get
        {
            return new Vector2(App.Width / 2, App.Height / 2);
        }
    }

    /// <summary>
    /// The camera zoom
    /// </summary>
    public float Zoom { get; set; }

    /// <summary>
    /// Camera rectangle boundary in the world space
    /// </summary>
    public Rect BoundingBox
    {
        get
        {
            return new Rect(
                x: Position.X - Origin.X / Zoom,
                y: Position.Y - Origin.Y / Zoom,
                w: App.Width / Zoom,
                h: App.Height / Zoom
            );
        }
    }

    /// <summary>
    /// The view matrix of the camera, use this on the batcher.PushMatrix() method to transform everything into the camera view
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
    ///  Project world space of this camera to screen space
    /// </summary>
    /// <param name="worldPosition">The world space position</param>
    public Vector2 ToScreenSpace(Vector2 worldPosition)
    {
        return Vector2.Transform(worldPosition, ViewMatrix);
    }

    /// <summary>
    ///  Project screen space to world space of this camera
    /// </summary>
    /// <param name="spacePosition">The screen space position</param>
    public Vector2 ToWorldSpace(Vector2 spacePosition)
    {
        Matrix3x2 invertedMatrix;

        if (!Matrix3x2.Invert(ViewMatrix, out invertedMatrix))
        {
            throw new Exception("Cannot convert screen space to world space, The camera view matrix is not invertible");    
        }
        
        return Vector2.Transform(spacePosition, invertedMatrix);
    }

    /// <summary>
    /// The constructor
    /// </summary>
    /// <param name="position">The position of the camera</param>
    /// <param name="rotation">The rotation of the camera</param>
    /// <param name="zoom">The camera zoom</param>
    public Camera2D(Vector2 position, float rotation, float zoom)
    {
        Position = position;
        Rotation = rotation;
        Zoom = zoom;
    }
}
