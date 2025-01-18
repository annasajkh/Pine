using Foster.Framework;
using Pine.Core.Interfaces;

namespace Pine.UI.Abstracts;

public class Panel : Widget, IRenderable
{
    public Color Color { get; set; } = Color.Transparent;
    public Color BorderColor { get; set; } = Color.Transparent;

    public int BorderThickness { get; set; } = 0;
    
    public int BorderRadius { get; set; }
    public int BorderRadiusTopLeft { get; set; }
    public int BorderRadiusTopRight { get; set; }
    public int BorderRadiusBottomRight { get; set; }
    public int BorderRadiusBottomLeft { get; set; }

    private int xWithPaddingWithBorder;
    private int yWithPaddingWithBorder;

    private int widthWithPaddingWithBorder;
    private int heightWithPaddingWithBorder;

    public Panel(int x, int y, int width, int height) : base(x, y, width, height)
    {

    }

    public override void Render(Batcher batcher)
    {
        if (BorderRadiusTopLeft == 0 && BorderRadiusTopRight == 0 && BorderRadiusBottomRight == 0 && BorderRadiusBottomLeft == 0)
        {
            if (Color != Color.Transparent)
            {
                batcher.RectRounded(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding, BorderRadius, BorderRadius, BorderRadius, BorderRadius, Color);
            }

            if (BorderColor != Color.Transparent)
            {
                batcher.RectRoundedLine(new Rect(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding), BorderRadius, BorderRadius, BorderRadius, BorderRadius, BorderThickness, BorderColor);
            }
        }
        else
        {
            if (Color != Color.Transparent)
            {
                batcher.RectRounded(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding, BorderRadiusTopLeft, BorderRadiusTopRight, BorderRadiusBottomRight, BorderRadiusBottomLeft, Color);
            }

            if (BorderColor != Color.Transparent && BorderThickness != 0)
            {
                batcher.RectRoundedLine(new Rect(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding), BorderRadiusTopLeft, BorderRadiusTopRight, BorderRadiusBottomRight, BorderRadiusBottomLeft, BorderThickness, BorderColor);
            }
        }

        base.Render(batcher);
    }
}