using Foster.Framework;

namespace Pine.Managers;

public sealed class ResourceManager : IDisposable
{
    private Dictionary<string, object> resources = new();

    public T Get<T>(string key) where T : notnull
    {
        object resource;
        T resourceTyped;

        try
        {
            resource = resources[key];
        }
        catch (Exception)
        {
            throw new KeyNotFoundException($"Resource doesn't exist in this resource manager");
        }

        try
        {
            resourceTyped = (T)resource;
        }
        catch (Exception)
        {
            throw new InvalidCastException($"Resource is not {typeof(T)}");
        }

        return resourceTyped;
    }

    public void Add<T>(string key, T resource) where T : notnull
    {
        resources.Add(key, resource);
    }

    public void Remove<T>(string key) where T : notnull
    {
        if (resources[key] is IResource resource)
        {
            resource.Dispose();
        }

        resources.Remove(key);
    }

    public void Dispose()
    {
        foreach (var item in resources)
        {
            if (item.Value is IResource resource)
            {
                resource.Dispose();
            }
        }
    }
}
