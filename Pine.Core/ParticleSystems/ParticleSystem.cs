using System.Numerics;
using Timer = Pine.Core.DataStructures.Timer;
using Pine.Core.Interfaces;
using Pine.Core.Components;

namespace Pine.Core.ParticleSystems;

public class ParticleSystem : IUpdateable, IRenderable
{
    public Vector2 Position { get; set; }
    public bool Oneshot { get; set; }
    public Timer SpawnTimer { get; }
    public bool Relative { get; set; }

    private List<Particle> particles;

    public ParticleSystem(Vector2 position, bool oneshot, bool relative, float spawnDelay)
    {
        Position = position;
        Oneshot = oneshot;
        Relative = relative;

        particles = new List<Particle>();

        SpawnTimer = new Timer(spawnDelay, oneshot);
        SpawnTimer.Start();
    }

    public void AddParticle(Particle particle)
    {
        if (Relative)
        {
            particle.Position -= Position;
        }

        particles.Add(particle);
    }

    public void Update(PineApplication pineApplication)
    {
        SpawnTimer.Update(pineApplication);

        for (int i = particles.Count - 1; i >= 0; i--)
        {
            particles[i].Update(pineApplication);

            if (particles[i].IsTimeout)
            {
                particles.Remove(particles[i]);
            }
        }
    }

    public void Render(PineApplication pineApplication)
    {
        if (Relative)
        {
            pineApplication.Batcher.PushMatrix(Position);
        }

        foreach (var particle in particles)
        {
            particle.Render(pineApplication);
        }

        if (Relative)
        {
            pineApplication.Batcher.PopMatrix();
        }
    }
}
