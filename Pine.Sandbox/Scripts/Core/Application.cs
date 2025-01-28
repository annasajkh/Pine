using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Scenes;
using PineSandbox.Scripts.Core.Scenes;
using Pine.Core.Managers;

namespace PineSandbox.Scripts.Core;

public class Application : App
{
    public SceneManager SceneManager { get; }
    public Batcher Batcher { get; } 

    public Application(string name, int width, int height, GraphicsDriver preferredGraphicsDriver) : base(new AppConfig(name, name, width, height, PreferredGraphicsDriver: preferredGraphicsDriver))
    {
        Batcher = new(GraphicsDevice);
        SceneManager = new SceneManager(this);
    }

    protected override void Startup()
    {
        SceneManager.AddScene<TestUI>("TestUI");
        SceneManager.AddScene<TestParticle>("TestParticle");

        SceneManager.SetActive("TestParticle");
    }

    protected override void Update()
    {
        SceneManager.Update();
    }

    protected override void Render()
    {
        SceneManager.Render(Batcher);
    }

    protected override void Shutdown()
    {

    }
}