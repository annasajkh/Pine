using Foster.Framework;
using System.Numerics;
using Color = Foster.Framework.Color;
using Pine.Interfaces;
namespace Pine.Components;

public sealed class Sprite : IRenderable
{
    /// <summary>
    /// The position of the sprite
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// The rotation of the sprite
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// The scale of the sprite
    /// </summary>
    public Vector2 Scale { get; set; }

    /// <summary>
    /// The origin of the sprite
    /// </summary>
    public Vector2 Origin { get; set; }

    /// <summary>
    /// The color of the sprite
    /// </summary>
    public Color Color { get; set; } = Color.White;

    /// <summary>
    /// How many rows this sprite have if it's a spritesheet
    /// </summary>
    public uint Rows { get; } = 1;

    /// <summary>
    /// How many columns this sprite have if it's a spritesheet
    /// </summary>
    public uint Columns { get; } = 1;

    /// <summary>
    /// The frame index of the sprite
    /// </summary>
    public uint FrameIndex { get; set; } = 0;

    /// <summary>
    /// toggle sprite is flip on the x axis
    /// </summary>
    public bool FlipX { get; set; }

    /// <summary>
    /// toggle sprite is flip on the y axis
    /// </summary>
    public bool FlipY { get; set; }

    /// <summary>
    /// the texture resource
    /// </summary>
    public Texture Texture { get; }

    /// <summary>
    /// Total number of frames
    /// </summary>
    public uint FrameCount
    {
        get
        {
            return Rows * Columns;
        }
    }

    private Rect sourceRect;

    /// <summary>
    /// The rectangle bounding box
    /// </summary>
    public Rect BoundingBox
    {
        get
        {
            return new Rect(Position.X - Origin.X, Position.Y - Origin.Y, Size.X, Size.Y);
        }
    }

    /// <summary>
    /// The size on the screen when it rendered
    /// </summary>
    public Vector2 Size
    {
        get
        {
            return new Vector2(sourceRect.Width * Scale.X, sourceRect.Height * Scale.Y);
        }
    }

    /// <summary>
    /// The constructor
    /// </summary>
    /// <param name="texture">The texture</param>
    /// <param name="position">The position of the sprite</param>
    /// <param name="rows">if you wish to make this a sprite sheet change this number to the row count of the sprite sheet</param>
    /// <param name="columns">if you wish to make this a sprite sheet change this number to the column count of the sprite sheet</param>
    public Sprite(Texture texture, Vector2 position = new Vector2(), uint rows = 1, uint columns = 1)
    {
        Position = position;
        Rotation = 0;
        Scale = Vector2.One;

        Rows = rows;
        Columns = columns;
        Texture = texture;

        sourceRect = new Rect(0, 0, (int)(Texture.Width / columns), (int)(Texture.Height / rows));

        Origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
    }

    /// <summary>
    /// Draw each frame to the screen the sprite sheet index is as follows
    /// |-----------|
    /// | 0 | 1 | 2 |
    /// |---|---|---|
    /// | 3 | 4 | 5 |
    /// |---|---|---|
    /// | 6 | 7 | 8 |
    /// |-----------|
    /// </summary>
    /// <param name="spriteBatch"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void Render(Batcher batcher)
    {
        if (FrameIndex > FrameCount - 1)
        {
            throw new IndexOutOfRangeException();
        }

        sourceRect.X = (int)(sourceRect.Width * (FrameIndex % Columns));
        sourceRect.Y = (int)(sourceRect.Height * (FrameIndex / Columns));

        Subtexture subtexture = new Subtexture(Texture, sourceRect);

        batcher.PushMatrix(Position, Scale, Origin, Rotation);
        batcher.ImageFit(subtexture, new Rect(0, 0, subtexture.Width, subtexture.Height), new Vector2(BoundingBox.Width, BoundingBox.Height), Color, FlipX, FlipY);
        batcher.PopMatrix();
    } 
}
