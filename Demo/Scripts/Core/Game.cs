using Demo.Scripts.Scenes;
using Foster.Extended.Managers;
using Foster.Framework;

namespace Demo.Scripts.Core;

public class Game : Module
{
    public static float Scale = 3;
    public static SceneManager SceneManager { get; } = new("MainMenu", new MainMenu());
    public static ResourceManager ResourceManager { get; } = new();
    public static bool ShowColliders { get; } = true;

    private Batcher batcher = new();

    public override void Startup()
    {
        ResourceManager.Add("ArialFont32", new SpriteFont(Path.Combine("Assets", "Fonts", "Arial.ttf"), 32));

        SceneManager.AddScene("World", new World());
    }

    public override void Update()
    {
        SceneManager.Update();
    }

    public override void Render()
    {
        Graphics.Clear(Color.Black);

        SceneManager.Render(batcher);

        batcher.Render();
        batcher.Clear();
    }

    public override void Shutdown()
    {
        ResourceManager.Dispose();
    }
}
