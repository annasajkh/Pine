using Foster.Framework;
using Pine.Components;
using Pine.Interfaces;

namespace Pine.Managers;

public sealed class SceneManager : IRenderable, IUpdateable
{
    /// <summary>
    /// The active scene is the one that is updating and drawing to the screen
    /// </summary>
    private Scene? activeScene;

    private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

    public SceneManager(string initialSceneName, Scene initialScene)
    {
        scenes.Add(initialSceneName, initialScene);

        activeScene = scenes[initialSceneName];
        activeScene.StartupInternal();
    }

    /// <summary>
    /// Add a scene to the scene manager
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <param name="scene">The scene</param>
    public void AddScene(string name, Scene scene)
    {
        scenes.Add(name, scene);
    }

    /// <summary>
    /// Remove a scene from the scene manager
    /// </summary>
    /// <param name="name">The scene name</param>
    /// <exception cref="Exception">Exception if the the scene manager doesn't have an active scene or if you trying to remove an active scene</exception>
    public void RemoveScene(string name)
    {
        if (activeScene == null)
        {
            throw new Exception("Scene manager doesn't contain active scene");
        }

        if (activeScene == scenes[name])
        {
            throw new Exception("Cannot unload active scene");
        }

        activeScene.ShutdownInternal();
        scenes.Remove(name);
    }

    /// <summary>
    /// Change to different scene
    /// </summary>
    /// <param name="name">The name of the scene</param>
    /// <exception cref="Exception">Exception if the scene manager doesn't have an active scene</exception>
    public void ChangeScene(string name)
    {
        if (activeScene == null)
        {
            throw new Exception("Scene Manager doesn't contain active scene");
        }

        activeScene.ShutdownInternal();
        activeScene = (Scene)Activator.CreateInstance(scenes[name].GetType())!;
        activeScene.StartupInternal();
    }

    public void Update()
    {
        activeScene?.Update();
    }

    public void Render(Batcher batcher)
    {
        activeScene?.Render(batcher);
    }
}
