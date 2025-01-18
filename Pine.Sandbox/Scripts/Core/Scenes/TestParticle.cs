using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Particles;
using System.Numerics;
using Pine.Core.Components;
using Pine.Core.ParticleSystems;

namespace Pine.Sandbox.Scripts.Core.Scenes;

public class TestParticle : Scene
{
    ParticleSystem particleSystem;

    public override void Startup()
    {
        ClearColor = Color.Black;

        particleSystem = new ParticleSystem(position: Input.Mouse.Position, oneshot: false, relative: true, spawnDelay: 0.01f);

        particleSystem.SpawnTimer.OnTimeout += () =>
        {
            particleSystem.Particles.Add(new RotatingRect(particleSystem.Position, new Vector2(20, 20), 1));
        };

    }

    public override void Update()
    {
        particleSystem.Position = Input.Mouse.Position;

        particleSystem.Update();
    }

    public override void Render(Batcher batcher)
    {
        particleSystem.Render(batcher);
    }

    public override void Shutdown()
    {

    }
}
