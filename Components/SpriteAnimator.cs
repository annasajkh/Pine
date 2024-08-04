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
    /// Determine if the animation is looping
    /// </summary>
    public bool Looping { get; }
    
    /// <summary>
    /// Determine if the animation is played in reverse
    /// </summary>
    public bool Reversed { get; set; }
    
    /// <summary>
    /// The frame index of the animation, this changes more rapidly the higher the FramePerSecond
    /// </summary>
    public int FrameIndex { get; private set; }
    
    /// <summary>
    /// The sprite sheet that will be use for the animation
    /// </summary>
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
    
    /// <summary>
    /// Play the animation
    /// </summary>
    public void Play()
    {
        Playing = true;
    }
    
    /// <summary>
    /// Stop the animation
    /// </summary>
    public void Stop()
    {
        Playing = false;
    }
    
    /// <summary>
    /// Update the animation so that it animate duh
    /// </summary>
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
