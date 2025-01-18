using Foster.Framework;
using Pine.Core.Interfaces;

namespace Pine.UI.Abstracts;

public class Widget : IUpdateable, IRenderable
{
    /// <summary>
    /// The tag that will be use to get this widget
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// The pure x position of this widget
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The pure y position of this widget
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The pure width of this widget
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The pure height of this widget
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The margin of this widget
    /// </summary>
    public int Margin { get; set; }

    /// <summary>
    /// The padding of this widget
    /// </summary>
    public int Padding { get; set; }

    /// <summary>
    /// The margin left of this widget
    /// </summary>
    public int MarginLeft { get; set; }

    /// <summary>
    /// The margin right of this widget
    /// </summary>
    public int MarginRight { get; set; }

    /// <summary>
    /// The margin top of this widget
    /// </summary>
    public int MarginTop { get; set; }

    /// <summary>
    /// The margin bottom of this widget
    /// </summary>
    public int MarginBottom { get; set; }

    /// <summary>
    /// The padding left of this widget
    /// </summary>
    public int PaddingLeft { get; set; }

    /// <summary>
    /// The padding right of this widget
    /// </summary>
    public int PaddingRight { get; set; }

    /// <summary>
    /// The padding top of this widget
    /// </summary>
    public int PaddingTop { get; set; }

    /// <summary>
    /// The padding bottom of this widget
    /// </summary>
    public int PaddingBottom { get; set; }

    /// <summary>
    /// This get called when the mouse entered this widget
    /// </summary>
    public Action OnMouseEnter { get; set; }

    /// <summary>
    /// This get called when the mouse exited this widget
    /// </summary>
    public Action OnMouseExit { get; set; }

    /// <summary>
    /// This get called when the mouse left button click this widget
    /// </summary>
    public Action OnMousePressed { get; set; }

    /// <summary>
    /// This get called when the mouse left button pressing this widget
    /// </summary>
    public Action OnMouseDown { get; set; }

    /// <summary>
    /// This get called when the mouse left button unpressing this widget
    /// </summary>
    public Action OnMouseReleased { get; set; }

    /// <summary>
    /// Toggle drawing original widget, margin, and padding area
    /// </summary>
    public bool DebugDraw { get; set; }

    private bool pointerInsideOnce = false;
    private bool pointerOutsideOnce = true;

    protected int xWithPadding;
    protected int yWithPadding;
    protected int widthWithPadding;
    protected int heightWithPadding;

    protected int xWithPaddingWithMargin;
    protected int yWithPaddingWithMargin;
    protected int widthWithPaddingWithMargin;
    protected int heightWithPaddingWithMargin;

    public int FullX
    {
        get
        {
            return xWithPaddingWithMargin;
        }
    }

    public int FullY
    {
        get
        {
            return yWithPaddingWithMargin;
        }
    }

    public int FullWidth
    {
        get
        {
            return widthWithPaddingWithMargin;
        }
    }

    public int FullHeight
    {
        get
        {
            return heightWithPaddingWithMargin;
        }
    }

    public Widget(int x, int y, int width, int height)
    {
        X = x;
        Y = y;

        Width = width;
        Height = height;
    }

    public virtual void Update()
    {
        xWithPadding = X;
        yWithPadding = Y;

        widthWithPadding = Width;
        heightWithPadding = Height;

        if (PaddingLeft == 0 && PaddingRight == 0 && PaddingTop == 0 && PaddingBottom == 0)
        {
            xWithPadding -= Padding;
            yWithPadding -= Padding;

            widthWithPadding += Padding * 2;
            heightWithPadding += Padding * 2;
        }
        else
        {
            xWithPadding -= PaddingLeft;
            yWithPadding -= PaddingTop;

            widthWithPadding += PaddingRight * 2;
            heightWithPadding += PaddingBottom * 2;
        }

        xWithPaddingWithMargin = xWithPadding;
        yWithPaddingWithMargin = yWithPadding;
        widthWithPaddingWithMargin = widthWithPadding;
        heightWithPaddingWithMargin = heightWithPadding;

        if (MarginLeft == 0 && MarginRight == 0 && MarginTop == 0 && MarginBottom == 0)
        {
            xWithPaddingWithMargin -= Margin;
            yWithPaddingWithMargin -= Margin;

            widthWithPaddingWithMargin += Margin * 2;
            heightWithPaddingWithMargin += Margin * 2;
        }
        else
        {
            xWithPaddingWithMargin -= MarginLeft;
            yWithPaddingWithMargin -= MarginTop;

            widthWithPaddingWithMargin += MarginRight * 2;
            heightWithPaddingWithMargin += MarginBottom * 2;
        }

        RectInt widgetRect = new(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding);

        if (widgetRect.Contains(Input.Mouse.Position))
        {    
            if (Input.Mouse.Down(MouseButtons.Left))
            {
                OnMouseDown?.Invoke();
            }
        }

        if (Input.Mouse.Released(MouseButtons.Left))
        {
            OnMouseReleased?.Invoke();
        }

        if (widgetRect.Contains(Input.Mouse.Position) && !pointerInsideOnce)
        {
            OnMouseEnter?.Invoke();

            if (Input.Mouse.Pressed(MouseButtons.Left))
            {
                OnMousePressed?.Invoke();
            }

            pointerInsideOnce = true;
            pointerOutsideOnce = false;
        }

        if (!widgetRect.Contains(Input.Mouse.Position) && !pointerOutsideOnce)
        {
            OnMouseExit?.Invoke();

            pointerOutsideOnce = true;
            pointerInsideOnce = false;
        }
    }

    public virtual void Render(Batcher batcher)
    {
        if (DebugDraw)
        {
            batcher.Rect(xWithPaddingWithMargin, yWithPaddingWithMargin, widthWithPaddingWithMargin, heightWithPaddingWithMargin, new Color(176, 131, 84, 127));
            batcher.Rect(xWithPadding, yWithPadding, widthWithPadding, heightWithPadding, new Color(99, 126, 87, 127));
            batcher.Rect(X, Y, Width, Height, new Color(87, 125, 159, 127));
        }
    }
}