using Foster.Framework;

namespace Pine.Core.Interfaces;

/// <summary>
/// Something that can be render should implement this interface.
/// </summary>
public interface IRenderable
{
    public void Render(App app, Batcher batcher);
}
