using System.Numerics;
using Timer = Pine.Core.DataStructures.Timer;
using Pine.Core.Interfaces;
using Pine.Core.Components;

namespace Pine.Core.ParticleSystems;

public abstract class Particle : IUpdateable, IRenderable
{
    public Vector2 Position { get; set; }
    public Vector2 Rotation { get; set; }

    public bool IsTimeout { get; private set; }
    public float Lifetime { get; set; }

    public float TimeLeft
    {
        get
        {
            return lifetimeTimer.TimeLeft;
        }
    }

    public float TimeLeftNormal
    {
        get
        {
            return lifetimeTimer.TimeLeft / Lifetime;
        }
    }

    private Timer lifetimeTimer;

    public Particle(Vector2 position, float lifetime)
    {
        Position = position;
        Lifetime = lifetime;

        lifetimeTimer = new Timer(lifetime, true);
        lifetimeTimer.OnTimeout += () =>
        {
            IsTimeout = true;
        };

        lifetimeTimer.Start();
    }

    public virtual void Update(PineApplication pineApplication)
    {
        lifetimeTimer.Update(pineApplication);
    }

    public abstract void Render(PineApplication pineApplication);
}
