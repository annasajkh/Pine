using Foster.Framework;
using Pine.Sandbox.Scripts.Core.Scenes;
using PineSandbox.Scripts.Core.Scenes;
using Pine.Core.Managers;

namespace PineSandbox.Scripts.Core;

public class Application : Module
{
    SceneManager SceneManager { get; } = new();
    Batcher Batcher { get; } = new();

    public override void Startup()
    {
        SceneManager.AddScene<TestUI>("TestUI");
        SceneManager.AddScene<TestParticle>("TestParticle");
        SceneManager.AddScene<TestImGUI>("TestImGUI");

        SceneManager.SetActive("TestParticle");
    }

    public override void Update()
    {
        SceneManager.Update();
    }

    public override void Render()
    {
        SceneManager.Render(Batcher);
    }

    public override void Shutdown()
    {

    }
}
