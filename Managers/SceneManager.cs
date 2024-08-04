using System;
using System.Collections.Generic;
using Foster.Framework;
using Pine.Components;
using Pine.Interfaces;

namespace Pine.Managers;

public sealed class SceneManager : IRenderable, IUpdateable
{
    private Scene? activeScene;
    private readonly Dictionary<string, Func<Scene>> sceneLambdas = new();
    
    /// <summary>
    /// The constructor
    /// When the constructor is called it will set the activeScene to be the initial scene
    /// </summary>
    /// <param name="initialSceneName">The initial scene name</param>
    /// <param name="initialSceneLambda">This lambda should return a new scene when it's called ex () => new MainMenu() </param>
    public SceneManager(string initialSceneName, Func<Scene> initialSceneLambda)
    {
        AddScene(initialSceneName, initialSceneLambda);
        ChangeScene(initialSceneName);
    }
    
    /// <summary>
    /// Add a new scene to this scene manager
    /// </summary>
    /// <param name="name">The scene name</param>
    /// <param name="sceneLambda">This lambda should return a new scene when it's called ex () => new MainMenu() </param>
    /// <exception cref="ArgumentException">The exception will be thrown when the scene already exist in this manager</exception>
    public void AddScene(string name, Func<Scene> sceneLambda)
    {
        if (sceneLambdas.ContainsKey(name))
        {
            throw new ArgumentException($"Scene '{name}' already exists.", nameof(name));
        }
        
        sceneLambdas[name] = sceneLambda;
    }
    
    /// <summary>
    /// Remove a scene from this scene manager
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <exception cref="InvalidOperationException">This will be thrown when you try to remove an active scene</exception>
    /// <exception cref="ArgumentException">This will get thrown when the scene you are trying to remove doesn't exist</exception>
    public void RemoveScene(string name)
    {
        if (activeScene?.GetType().Name == name)
        {
            throw new InvalidOperationException("Cannot remove the active scene.");
        }
        
        if (!sceneLambdas.Remove(name))
        {
            throw new ArgumentException($"Scene '{name}' does not exist.", nameof(name));
        }
    }
    
    /// <summary>
    /// Changing the active scene to any other scene in this scene manager
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <exception cref="ArgumentException">This will be thrown when the scene doesn't exist in the scene manager</exception>
    public void ChangeScene(string name)
    {
        if (!sceneLambdas.TryGetValue(name, out var sceneLambda))
        {
            throw new ArgumentException($"Scene '{name}' does not exist.", nameof(name));
        }

        activeScene?.ShutdownInternal();
        activeScene = sceneLambda();
        activeScene.StartupInternal();
    }
    
    /// <summary>
    /// Update the active scene
    /// </summary>
    public void Update()
    {
        activeScene?.Update();
    }
    
    /// <summary>
    /// Render the active scene
    /// </summary>
    /// <param name="batcher">The batcher</param>
    public void Render(Batcher batcher)
    {
        if (activeScene is not null)
        {
            Graphics.Clear(activeScene.ClearColor);
        
            activeScene.Render(batcher);
        
            batcher.Render();
            batcher.Clear();   
        }
    }
}