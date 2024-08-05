using System.Numerics;
using Foster.Framework;
using Pine.Interfaces;

namespace Pine.Components;

/// <summary>
/// Anything that can exist in the world is a GameObject
/// </summary>
public abstract class GameObject : IUpdateable, IDisposable
{
    /// <summary>
    /// The position of the game object
    /// </summary>
    public Vector2 position;
    
    /// <summary>
    /// The rotation of the game object
    /// </summary>
    public float rotation;
    
    /// <summary>
    /// The scale of the game object
    /// </summary>
    public Vector2 scale;
    
    /// <summary>
    /// The size of the game object
    /// </summary>
    public Vector2 size;

    /// <summary>
    /// The rectangle that cover the entire object
    /// </summary>
    public Rect BoundingBox
    {
        get
        {
            return new Rect(position - scale * size / 2, position + scale * size / 2);
        }
    }

    public GameObject(Vector2 position, float rotation, Vector2 scale, Vector2 size)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.size = size;
    }

    public abstract void Update();

    public abstract void Dispose();
}