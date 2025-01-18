using Foster.Framework;
using Pine.UI.Abstracts;

namespace Pine.UI.Containers;

public class CenterContainer : Widget
{
    public Widget? Child { get; set; }

    public CenterContainer(int x, int y, int width, int height, Widget? child = null) : base(x, y, width, height)
    {
        Child = child;
    }

    public override void Update()
    {
        base.Update();
        Child.Update();

        Child.X = FullX + FullWidth / 2 - Child.FullWidth / 2;
        Child.Y = FullY + FullHeight / 2 - Child.FullHeight / 2;

    }

    public override void Render(Batcher batcher)
    {
        base.Render(batcher);
        Child.Render(batcher);
    }
}