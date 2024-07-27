using Demo.Scripts.Scenes;
using Foster.Framework;
using System.Numerics;

namespace Demo.Scripts.GameObjects.Entities;

public class Entity : GameObject
{
    public float Speed { get; }
    public World World { get; }
    public Vector2 Velocity { get; protected set; }

    public Entity(Vector2 position, Vector2 size, float speed, World world) : base(position, 0, Vector2.One, size)
    {
        World = world;
        Speed = speed;
    }

    public override void Update()
    {
        position += Velocity * Time.Delta;
    }
}
