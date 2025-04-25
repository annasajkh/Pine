using Foster.Framework;
using Pine.Core.Managers;

namespace Pine.Core.Components;

public abstract class PineApplication : App
{
    public SceneManager SceneManager { get; private set; }
    public Batcher Batcher { get; private set; }
    public ResourceManager ResourceManager { get; private set; } = new();

    protected PineApplication(AppConfig appConfig) : base(appConfig)
    {
        Batcher = new(GraphicsDevice);
        SceneManager = new SceneManager(this);
    }

    protected override void Update()
    {
        SceneManager.Update();
    }

    protected override void Render()
    {
        SceneManager.Render(Batcher);
    }
}
