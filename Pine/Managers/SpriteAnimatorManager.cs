using Foster.Extended.Components;

namespace Foster.Extended.Managers;

public sealed class SpriteAnimatorManager
{
    private Dictionary<string, SpriteAnimator> spriteAnimators = new();
    private string? currentAnimator;

    /// <summary>
    /// The current animator that will be playing if you call Play()
    /// </summary>
    public SpriteAnimator? CurrentAnimator
    {
        get
        {
            if (currentAnimator is null)
            {
                return null;
            }

            return spriteAnimators[currentAnimator];
        }
    }

    public void SetCurrent(string name)
    {
        if (!spriteAnimators.ContainsKey(name))
        {
            throw new Exception($"Sprite animator controller doesn't contains {name}");
        }

        currentAnimator = name;
    }

    public void AddAnimator(string name, SpriteAnimator spriteAnimator)
    {
        spriteAnimators.Add(name, spriteAnimator);
    }

    public void RemoveAnimator(string name)
    {
        spriteAnimators.Remove(name);
    }

    /// <summary>
    /// Play the current sprite animator
    /// </summary>
    public void Play()
    {
        if (currentAnimator is null)
        {
            return;
        }

        spriteAnimators[currentAnimator].Play();
    }


    /// <summary>
    /// Stop the current sprite animator
    /// </summary>
    public void Stop()
    {
        if (currentAnimator is null)
        {
            return;
        }

        spriteAnimators[currentAnimator].Stop();
    }


    /// <summary>
    /// Update the current sprite animator
    /// </summary>
    public void Update()
    {
        if (currentAnimator is null)
        {
            return;
        }

        spriteAnimators[currentAnimator].Update();
    }
}
