﻿using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Particles;
using System.Numerics;
using Pine.Core.Components;
using Pine.Core.ParticleSystems;

namespace Pine.Sandbox.Scripts.Core.Scenes;

public class TestParticle : Scene
{
    ParticleSystem particleSystem;

    public override void Startup(PineApplication pineApplication)
    {
        ClearColor = Color.Black;

        particleSystem = new ParticleSystem(position: pineApplication.Input.Mouse.Position, oneshot: false, relative: false, spawnDelay: 0.001f);

        particleSystem.SpawnTimer.OnTimeout += () =>
        {
            particleSystem.AddParticle(new RotatingRect(position: particleSystem.Position, size: new Vector2(20, 20), lifetime: 1));
        };

    }

    public override void Update(PineApplication pineApplication)
    {
        particleSystem.Position = pineApplication.Input.Mouse.Position;

        particleSystem.Update(pineApplication);
    }

    public override void Render(PineApplication pineApplication)
    {
        particleSystem.Render(pineApplication);
    }

    public override void Shutdown(PineApplication pineApplication)
    {

    }
}
