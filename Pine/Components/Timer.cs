using Foster.Extended.Interfaces;
using Foster.Framework;

namespace Foster.Extended.Components;

public sealed class Timer : IUpdateable
{
    /// <summary>
    /// The time the timer have to wait before it timeout
    /// </summary>
    public float WaitTime { get; set; }

    /// <summary>
    /// Whether or not it's only timeout once
    /// </summary>
    public bool Oneshot { get; set; }
    
    /// <summary>
    /// The time it have left before it timeout
    /// </summary>
    public float TimeLeft { get; private set; }

    /// <summary>
    /// Whether or not it's paused
    /// </summary>
    public bool Paused { get; private set; } = true;

    /// <summary>
    /// Trigged when TimeLeft reached 0
    /// </summary>
    public event Action? OnTimeout;

    /// <summary>
    /// The constructor
    /// </summary>
    /// <param name="waitTime">The time before it timeout (in seconds)</param>
    /// <param name="oneshot">Whether or not it's only timeout once</param>
    public Timer(float waitTime, bool oneshot)
    {
        WaitTime = waitTime;
        Oneshot = oneshot;
    }

    /// <summary>
    /// Start the timer
    /// </summary>
    public void Start()
    {
        Paused = false;
    }
    
    /// <summary>
    /// Stop the timer
    /// </summary>
    public void Stop()
    {
        Paused = true;
    }

    /// <summary>
    /// Update the timer
    /// </summary>
    public void Update()
    {
        if (!Paused)
        {
            TimeLeft += Time.Delta;

            if (TimeLeft >= WaitTime)
            {
                TimeLeft = 0;

                if (OnTimeout != null)
                {
                    OnTimeout();
                }

                if (Oneshot)
                {
                    Paused = true;
                }
            }
        }
    }
}
