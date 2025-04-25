using Foster.Framework;
using System.Numerics;
using Pine.Core.Extensions;
using Pine.Core.ParticleSystems;
using Pine.Core.Utils;
using Pine.Core.Components;

namespace Pine.Sandbox.Scripts.Core.Particles;

public class RotatingRect : Particle
{
    Color color;
    Vector2 size;
    float rotation;
    float speed = 100;
    Vector2 direction;

    public RotatingRect(Vector2 position, Vector2 size, float lifetime) : base(position, lifetime)
    {
        direction = new Vector2(1, 0).RotateDegrees(RandomHelper.NextFloat() * 360);

        this.size = size;
    }

    public override void Update(PineApplication pineApplication)
    {
        color = ColorHelper.ColorFromHSV(TimeLeftNormal, 1, 1);

        Position += direction * speed * pineApplication.Time.Delta;
        rotation += 10 * pineApplication.Time.Delta;
        base.Update(pineApplication);
    }

    public override void Render(PineApplication pineApplication)
    {
        pineApplication.Batcher.PushMatrix(Position, Vector2.One, new Vector2(size.X / 2 * (1 - TimeLeftNormal), size.Y / 2 * (1 - TimeLeftNormal)), rotation);
        pineApplication.Batcher.Rect(0, 0, size.X * (1 - TimeLeftNormal), size.Y * (1 - TimeLeftNormal), color);
        pineApplication.Batcher.PopMatrix();
    }
}
