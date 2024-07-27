using Demo.Scripts.Core;
using Demo.Scripts.Scenes;
using Foster.Extended.Components;
using Foster.Extended.Utils;
using Foster.Framework;
using System.Numerics;
using Timer = Foster.Extended.Components.Timer;

namespace Demo.Scripts.GameObjects.Entities;

public class Player : Entity
{
    public Camera2D Camera { get; }
    public Action FireBullet;
    
    Timer bulletFireTimer;
    SpriteAnimator playerAnimator;
    Sprite sprite;

    public Player(Vector2 position, World world) : base(position, new Sprite(world.ResourceManager.Get<Texture>("player_texture"), rows: 2, columns: 5).Size, 200, world)
    {
        sprite = new Sprite(world.ResourceManager.Get<Texture>("player_texture"), rows: 2, columns: 5);
        sprite.Scale = new Vector2(Game.Scale, Game.Scale);

        playerAnimator = new SpriteAnimator(sprite, framePerSecond: 20, frameIndices: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9], looping: true);
        playerAnimator.Play();

        bulletFireTimer = new Timer(waitTime: 0.5f, oneshot: false);
        bulletFireTimer.OnTimeout += () =>
        {
            FireBullet?.Invoke();
        };
        bulletFireTimer.Start();

        Camera = new Camera2D(position, 0, 1);
    }

    public override void Update()
    {
        Vector2 lookDir = (Input.Mouse.Position - Camera.ToScreenSpace(position)).Normalized();

        rotation = lookDir.Angle() + MathHelper.DegreesToRadians(90);

        bulletFireTimer.Update();
        playerAnimator.Update();

        Vector2 direction = Vector2.Zero;

        if (Input.Keyboard.Down(Keys.D))
        {
            direction.X = 1;
        }
        else if (Input.Keyboard.Down(Keys.A))
        {
            direction.X = -1;
        }

        if (Input.Keyboard.Down(Keys.W))
        {
            direction.Y = -1;
        }
        else if (Input.Keyboard.Down(Keys.S))
        {
            direction.Y = 1;
        }

        Velocity = direction.Normalized() * Speed;
        base.Update();

        sprite.Position = position;
        sprite.Rotation = rotation;

        Camera.Position = position;
    }

    public override void Render(Batcher batcher)
    {
        sprite.Render(batcher);
    }
}
