using Foster.Framework;
using Pine.UI.Abstracts;
using Pine.UI.Containers;
using Pine.Core.Components;

namespace PineSandbox.Scripts.Core.Scenes;


public class TestUI : Scene
{
    Panel panel = new(x: 300, y: 300, width: 100, height: 100);
    CenterContainer centerContainer = new(0, 0, App.Width, App.Height);

    public override void Startup()
    {
        ClearColor = Color.FromHexStringRGBA("#1e2531");

        panel.Color = Color.FromHexStringRGBA("#19202a");
        panel.BorderColor = Color.FromHexStringRGBA("#30363d");
        panel.BorderThickness = 1;
        panel.BorderRadius = 8;
        //panel.DebugDraw = true;
        panel.Padding = 10;
        panel.Margin = 10;

        panel.OnMouseEnter += () =>
        {
            panel.Color = Color.FromHexStringRGBA("#222a35");
            panel.BorderColor = Color.FromHexStringRGBA("#3a414a");

            Console.WriteLine("Mouse Enter");
        };

        panel.OnMouseExit += () =>
        {
            panel.Color = Color.FromHexStringRGBA("#19202a");
            panel.BorderColor = Color.FromHexStringRGBA("#30363d");

            Console.WriteLine("Mouse Exit");
        };

        //centerContainer.DebugDraw = true;
        centerContainer.Child = panel;
    }

    public override void Update()
    {
        centerContainer.Width = App.Width;
        centerContainer.Height = App.Height;

        centerContainer.Update();
    }

    public override void Render(Batcher batcher)
    {
        centerContainer.Render(batcher);
    }

    public override void Shutdown()
    {

    }
}
