using Foster.Framework;

namespace Pine.Managers;

public sealed class ResourceManager : IDisposable
{
    private readonly Dictionary<string, object> resources = new();
    
    /// <summary>
    /// Get a resource
    /// </summary>
    /// <exception cref="KeyNotFoundException">Thrown when the eesource name is not found </exception>
    /// <exception cref="InvalidCastException">Thrown when you try to access the resource with by passing T type but the resource is not T type</exception>
    public T Get<T>(string name) where T : notnull
    {
        object resource;
        T resourceTyped;

        try
        {
            resource = resources[name];
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
    
    /// <summary>
    /// Add a new resource
    /// </summary>
    /// <param name="name">The name of the resource</param>
    /// <param name="resource">the instance of the resource itself</param>
    /// <typeparam name="T">the type of the resource usually this is inferred automatically</typeparam>
    public void Add<T>(string name, T resource) where T : notnull
    {
        resources.Add(name, resource);
    }
    
    /// <summary>
    /// Remove a resource
    /// </summary>
    /// <param name="name">The name of the resource</param>
    /// <typeparam name="T">the type of the resource usually this is inferred automatically</typeparam>
    public void Remove<T>(string name) where T : notnull
    {
        if (resources[name] is IResource resource)
        {
            resource.Dispose();
        }

        resources.Remove(name);
    }
    
    /// <summary>
    /// Dispose all the resource, this will call the Dispose method of the resource if it's an IResource
    /// </summary>
    public void Dispose()
    {
        foreach (KeyValuePair<string, object> item in resources)
        {
            if (item.Value is IResource resource)
            {
                resource.Dispose();
            }
        }
    }
}
