using Foster.Framework;

namespace Pine.Components;

/// <summary>
/// Scene is a place where things happened like the main menu or the game world
/// </summary>
public abstract class Scene
{
    /// <summary>
    /// Get called when the scene is initialized
    /// or when changing scene where this scene is the target of the changing scene proccess
    /// </summary>
    public abstract void Startup();
    
    /// <summary>
    /// Get called every frame
    /// </summary>
    public abstract void Update();
    
    /// <summary>
    /// Get called after Update()
    /// </summary>
    /// <param name="batcher"></param>
    public abstract void Render(Batcher batcher);
    
    /// <summary>
    /// Get called when the scene is shutting down or when changing scenes
    /// where this scene is the one that is being unloaded
    /// unmanaged resources per scene shouldn't be permanent and should gone when changing scene
    /// </summary>
    public abstract void Shutdown();

    public void StartupInternal()
    {
        Startup();
        Log.Info($"{GetType().Name} Scene is Loaded");
    }

    public void ShutdownInternal()
    {
        Shutdown();
        Log.Info($"{GetType().Name} Scene is Unloaded");
    }
}
