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

    public override void Update(App app)
    {
        base.Update(app);
        Child.Update(app);

        Child.X = FullX + FullWidth / 2 - Child.FullWidth / 2;
        Child.Y = FullY + FullHeight / 2 - Child.FullHeight / 2;

    }

    public override void Render(App app, Batcher batcher)
    {
        base.Render(app, batcher);
        Child.Render(app, batcher);
    }
}