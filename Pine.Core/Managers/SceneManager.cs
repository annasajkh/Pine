using Foster.Framework;
using Pine.Core.Components;

namespace Pine.Core.Managers;

public sealed class SceneManager
{
    private App app;
    private Scene? activeScene;
    private readonly Dictionary<string, Func<Scene>> sceneLambdas = new();

    public SceneManager(App app)
    {
        this.app = app;
    }
    
    /// <summary>
    /// Add a new scene to this scene manager.
    /// </summary>
    /// <param name="name">The scene name.</param>
    /// <exception cref="ArgumentException">The exception will be thrown when the scene already exist in this manager.</exception>
    public void AddScene<T>(string name) where T : Scene, new()
    {
        if (sceneLambdas.ContainsKey(name))
        {
            throw new InvalidOperationException($"Scene with the name '{name}' already exists.");
        }
        
        sceneLambdas.Add(name, () => new T());
    }
    
    /// <summary>
    /// Remove a scene from this scene manager.
    /// </summary>
    /// <param name="name">The name of the scene.</param>
    /// <exception cref="InvalidOperationException">This will be thrown when you try to remove an active scene.</exception>
    /// <exception cref="ArgumentException">This will get thrown when the scene you are trying to remove doesn't exist.</exception>
    public void RemoveScene(string name)
    {
        if (activeScene?.GetType().Name == name)
        {
            throw new InvalidOperationException("Cannot remove the active scene.");
        }
        
        if (!sceneLambdas.Remove(name))
        {
            throw new KeyNotFoundException($"Scene with the name '{name}' does not exist.");
        }
    }
    
    /// <summary>
    /// Set the active scene to any other scene in this scene manager.
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <exception cref="ArgumentException">This will be thrown when the scene doesn't exist in the scene manager.</exception>
    public void SetActive(string name)
    {
        if (!sceneLambdas.TryGetValue(name, out  Func<Scene>? sceneLambda))
        {
            throw new KeyNotFoundException($"Scene with the name '{name}' does not exist.");
        }

        activeScene?.ShutdownInternal(app);
        activeScene = sceneLambda();
        activeScene.StartupInternal(app);
    }
    
    /// <summary>
    /// Update the active scene.
    /// </summary>
    public void Update()
    {
        activeScene?.Update(app);
    }
    
    /// <summary>
    /// Render the active scene.
    /// </summary>
    /// <param name="batcher">The batcher.</param>
    public void Render(Batcher batcher)
    {
        if (activeScene is not null)
        {
            app.Window.Clear(activeScene.ClearColor);
        
            activeScene.Render(app, batcher);
        
            batcher.Render(app.Window);
            batcher.Clear();   
        }
    }
}
