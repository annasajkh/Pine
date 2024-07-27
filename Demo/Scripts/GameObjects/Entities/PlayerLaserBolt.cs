using Demo.Scripts.Core;
using Demo.Scripts.Scenes;
using Foster.Extended.Components;
using Foster.Framework;
using System.Numerics;

namespace Demo.Scripts.GameObjects.Entities.Bullets;

public class PlayerLaserBolt : Entity
{
    SpriteAnimator laserAnimator;
    Sprite sprite;

    public PlayerLaserBolt(Vector2 position, World world) : base(position, new Sprite(world.ResourceManager.Get<Texture>("laser_bolts"), rows: 2, columns: 2).Size, 400, world)
    {
        sprite = new Sprite(world.ResourceManager.Get<Texture>("laser_bolts"), rows: 2, columns: 2);
        sprite.Scale = new Vector2(Game.Scale, Game.Scale);

        laserAnimator = new SpriteAnimator(sprite, 20, [2, 3], true);
    }

    public override void Update()
    {
        laserAnimator.Update();

        Velocity = Vector2.UnitY * -Speed;

        base.Update();
        sprite.Position = position;
    }

    public override void Render(Batcher batcher)
    {
        sprite.Render(batcher);
    }
}
