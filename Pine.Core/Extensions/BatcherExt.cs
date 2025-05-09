﻿using Foster.Framework;
using System.Numerics;

namespace Pine.Core.Extensions;

public static class BatcherExt
{
    /// <summary>
    /// Draw a centered text.
    /// </summary>
    /// <param name="batcher">The batcher.</param>
    /// <param name="text">The text.</param>
    /// <param name="position">The position.</param>
    /// <param name="color">The color.</param>
    /// <param name="font">The font.</param
    /// <param name="maxLineWidth">Max length of the text will wrap if it's more that the text length.</param>
    public static void TextCentered(this Batcher batcher, SpriteFont font, string text, Vector2 position, Color color, float maxLineWidth)
    {
        batcher.PushMatrix(position, Vector2.One, new Vector2(font.WidthOf(text) / 2, font.HeightOf(text) / 2), 0);
        batcher.Text(font, text, maxLineWidth, Vector2.Zero, color);
        batcher.PopMatrix();
    }


    /// <summary>
    /// Draw a centered text.
    /// </summary>
    /// <param name="batcher">The batcher.</param>
    /// <param name="text">The text.</param>
    /// <param name="position">The position.</param>
    /// <param name="color">The color.</param>
    /// <param name="font">The font.</param>
    public static void TextCentered(this Batcher batcher, SpriteFont font, string text, Vector2 position, Color color)
    {
        batcher.PushMatrix(position, Vector2.One, new Vector2(font.WidthOf(text) / 2, font.HeightOf(text) / 2), 0);
        batcher.Text(font, text, Vector2.Zero, color);
        batcher.PopMatrix();
    }
}
