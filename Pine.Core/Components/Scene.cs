﻿using Foster.Framework;
using Pine.Core.Interfaces;

namespace Pine.Core.Components;

/// <summary>
/// Scene is a place where things happened like the main menu or the game world.
/// </summary>
public abstract class Scene : IUpdateable, IRenderable
{
    /// <summary>
    /// The clear color for the scene.
    /// </summary>
    public Color ClearColor { get; protected set; } = Color.Black;
    
    /// <summary>
    /// Get called when the scene is initialized.
    /// or when changing scene where this scene is the target of the changing scene proccess.
    /// </summary>
    public abstract void Startup(PineApplication pineApplication);
    
    /// <summary>
    /// Get called every frame.
    /// </summary>
    public abstract void Update(PineApplication pineApplication);
    
    /// <summary>
    /// Get called after Update().
    /// </summary>
    /// <param name="batcher"></param>
    public abstract void Render(PineApplication pineApplication);
    
    /// <summary>
    /// Get called when the scene is shutting down or when changing scenes.
    /// where this scene is the one that is being unloaded.
    /// unmanaged resources per scene shouldn't be permanent and should be gone when changing scene.
    /// </summary>
    public abstract void Shutdown(PineApplication pineApplication);

    public void StartupInternal(PineApplication pineApplication)
    {
        Startup(pineApplication);
        Log.Info($"{GetType().Name} Scene is Loaded");
    }

    public void ShutdownInternal(PineApplication pineApplication)
    {
        Shutdown(pineApplication);
        Log.Info($"{GetType().Name} Scene is Unloaded");
    }
}
