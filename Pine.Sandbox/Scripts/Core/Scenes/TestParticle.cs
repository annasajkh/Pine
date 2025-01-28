using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Particles;
using System.Numerics;
using Pine.Core.Components;
using Pine.Core.ParticleSystems;

namespace Pine.Sandbox.Scripts.Core.Scenes;

public class TestParticle : Scene
{
    ParticleSystem particleSystem;

    public override void Startup(App app)
    {
        ClearColor = Color.Black;

        particleSystem = new ParticleSystem(position: app.Input.Mouse.Position, oneshot: false, relative: false, spawnDelay: 0.001f);

        particleSystem.SpawnTimer.OnTimeout += () =>
        {
            particleSystem.AddParticle(new RotatingRect(position: particleSystem.Position, size: new Vector2(20, 20), lifetime: 1));
        };

    }

    public override void Update(App app)
    {
        particleSystem.Position = app.Input.Mouse.Position;

        particleSystem.Update(app);
    }

    public override void Render(App app, Batcher batcher)
    {
        particleSystem.Render(app, batcher);
    }

    public override void Shutdown(App app)
    {

    }
}
