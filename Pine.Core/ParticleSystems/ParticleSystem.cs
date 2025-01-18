using Foster.Framework;
using System.Numerics;
using Timer = Pine.Core.Components.Timer;
using Pine.Core.Interfaces;

namespace Pine.Core.ParticleSystems;

public class ParticleSystem : IUpdateable, IRenderable
{
    public Vector2 Position { get; set; }
    public bool Oneshot { get; set; }
    public List<Particle> Particles { get; }
    public Timer SpawnTimer { get; }
    public bool Relative { get; set; }

    public ParticleSystem(Vector2 position, bool oneshot, bool relative, float spawnDelay)
    {
        Position = position;
        Oneshot = oneshot;
        Particles = new List<Particle>();
        Relative = relative;

        SpawnTimer = new Timer(spawnDelay, oneshot);
        SpawnTimer.Start();
    }

    public void Update()
    {
        SpawnTimer.Update();

        for (int i = Particles.Count - 1; i >= 0; i--)
        {
            Particles[i].Update();

            if (Particles[i].IsTimeout)
            {
                Particles.Remove(Particles[i]);
            }
        }
    }

    public void Render(Batcher batcher)
    {
        foreach (var particle in Particles)
        {
            particle.Render(batcher);
        }
    }
}
