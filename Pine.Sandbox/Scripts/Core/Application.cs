using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Scenes;
using Pine.Core.Managers;
using Pine.Core.Components;

namespace PineSandbox.Scripts.Core;

public class Application : PineApplication
{

    public Application(string name, int width, int height, GraphicsDriver preferredGraphicsDriver) : base(new AppConfig(name, name, width, height, PreferredGraphicsDriver: preferredGraphicsDriver))
    {

    }

    protected override void Startup()
    {
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