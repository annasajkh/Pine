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
    /// Add a new scene to this scene manager
    /// </summary>
    /// <param name="name">The scene name</param>
    /// <exception cref="ArgumentException">The exception will be thrown when the scene already exist in this manager</exception>
    public void AddScene<T>(string name) where T : Scene, new()
    {
        if (sceneLambdas.ContainsKey(name))
        {
            throw new InvalidOperationException($"Scene with the name '{name}' already exists.");
        }
        
        sceneLambdas.Add(name, () => new T());
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
            throw new KeyNotFoundException($"Scene with the name '{name}' does not exist.");
        }
    }
    
    /// <summary>
    /// Set the active scene to any other scene in this scene manager
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <exception cref="ArgumentException">This will be thrown when the scene doesn't exist in the scene manager</exception>
    public void SetActive(string name)
    {
        if (!sceneLambdas.TryGetValue(name, out var sceneLambda))
        {
            throw new KeyNotFoundException($"Scene with the name '{name}' does not exist.");
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