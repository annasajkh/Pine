using Pine.Core.Components;

namespace Pine.Core.Managers;

public sealed class SpriteAnimatorManager
{
    private Dictionary<string, SpriteAnimator> spriteAnimators = new();
    private string? activeAnimator;

    /// <summary>
    /// The active animator that will be playing if you call Play().
    /// </summary>
    public SpriteAnimator? ActiveAnimator
    {
        get
        {
            if (activeAnimator is null)
            {
                return null;
            }

            return spriteAnimators[activeAnimator];
        }
    }
    
    /// <summary>
    /// Set the active animator that will play.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="Exception"></exception>
    public void SetActive(string name)
    {
        if (!spriteAnimators.ContainsKey(name))
        {
            throw new Exception($"Sprite animator controller doesn't contains {name}");
        }

        activeAnimator = name;
    }
    
    /// <summary>
    /// Add an animator.
    /// </summary>
    /// <param name="name">The name of the animator.</param>
    /// <param name="spriteAnimator">The animator object.</param>
    public void AddAnimator(string name, SpriteAnimator spriteAnimator)
    {
        spriteAnimators.Add(name, spriteAnimator);
    }
    
    /// <summary>
    /// Remove an animator.
    /// </summary>
    /// <param name="name">The name of the animator.</param>
    public void RemoveAnimator(string name)
    {
        spriteAnimators.Remove(name);
    }
    
    /// <summary>
    /// Play the current sprite animator.
    /// </summary>
    public void Play()
    {
        if (activeAnimator is null)
        {
            return;
        }

        spriteAnimators[activeAnimator].Play();
    }
    
    /// <summary>
    /// Stop the current sprite animator.
    /// </summary>
    public void Stop()
    {
        if (activeAnimator is null)
        {
            return;
        }

        spriteAnimators[activeAnimator].Stop();
    }
    
    /// <summary>
    /// Update the current sprite animator.
    /// </summary>
    public void Update()
    {
        if (activeAnimator is null)
        {
            return;
        }

        spriteAnimators[activeAnimator].Update();
    }
}
