using Foster.Framework;
using Pine.Interfaces;

namespace Pine.Components;

/// <summary>
/// Grid based frame by frame sprite animation
/// </summary>
public sealed class SpriteAnimator : IUpdateable
{
    /// <summary>
    /// Frame indices of the sprite sheet to play
    /// </summary>
    public uint[] FrameIndices { get; }

    /// <summary>
    /// How many frame per second does this animator will play
    /// </summary>
    public float FramePerSecond { get; }
    
    /// <summary>
    /// Determine the animator is playing or now
    /// </summary>
    public bool Playing { get; private set; }

    /// <summary>
    /// Determine if
    /// </summary>
    public bool Looping { get; }
    public bool Reversed { get; set; }

    public int FrameIndex { get; private set; }
    public Sprite Sprite { get; }

    private float singleFrameElapsed;

    public SpriteAnimator(Sprite sprite, float framePerSecond, uint[] frameIndices, bool looping, bool reversed = false)
    {
        FrameIndices = frameIndices;
        FramePerSecond = framePerSecond;
        Reversed = reversed;
        Looping = looping;
        Sprite = sprite;
    }

    public void Play()
    {
        Playing = true;
    }

    public void Stop()
    {
        Playing = false;
    }

    public void Update()
    {
        // Update to the next frame
        if (Playing && singleFrameElapsed >= 1f / FramePerSecond)
        {
            FrameIndex += Reversed ? -1 : 1;

            singleFrameElapsed = 0;
        }

        singleFrameElapsed += Time.Delta;

        // Out of bounds handling and looping
        if (FrameIndex < 0)
        {
            if (Looping)
            {
                FrameIndex = FrameIndices.Length - 1;
            }
            else
            {
                FrameIndex = 0;
            }
        }
        else if (FrameIndex > FrameIndices.Length - 1)
        {
            if (Looping)
            {
                FrameIndex = 0;
            }
            else
            {
                FrameIndex = FrameIndices.Length - 1;
            }
        }

        Sprite.FrameIndex = FrameIndices[FrameIndex];
    }
}
