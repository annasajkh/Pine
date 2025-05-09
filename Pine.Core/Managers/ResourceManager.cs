﻿using DotNext;
using Foster.Framework;

namespace Pine.Core.Managers;


public enum ResourceManagerGetErrorCode
{
    Success = 0,
    ResourceNotExistInTheResourceManager,
    NameIsNull,
    InvalidResourceType
}

/// <summary>
/// The manager for resources.
/// </summary>
public sealed class ResourceManager : IDisposable
{
    private readonly Dictionary<string, object> resources = new();
    
    /// <summary>
    /// Get a resource
    /// </summary>
    public Result<T, ResourceManagerGetErrorCode> Get<T>(string name) where T : notnull
    {
        object resource;
        T resourceTyped;

        try
        {
            resource = resources[name];
        }
        catch (KeyNotFoundException)
        {
            return new Result<T, ResourceManagerGetErrorCode>(ResourceManagerGetErrorCode.ResourceNotExistInTheResourceManager);
        }
        catch (ArgumentNullException)
        {
            return new Result<T, ResourceManagerGetErrorCode>(ResourceManagerGetErrorCode.NameIsNull);
        }

        try
        {
            resourceTyped = (T)resource;
        }
        catch (InvalidCastException)
        {
            return new Result<T, ResourceManagerGetErrorCode>(ResourceManagerGetErrorCode.InvalidResourceType);
        }

        return resourceTyped;
    }
    
    /// <summary>
    /// Add a new resource.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="resource">the instance of the resource itself.</param>
    /// <typeparam name="T">the type of the resource usually this is inferred automatically.</typeparam>
    public void Add<T>(string name, T resource) where T : notnull
    {
        resources.Add(name, resource);
    }
    
    /// <summary>
    /// Remove a resource.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <typeparam name="T">the type of the resource usually this is inferred automatically.</typeparam>
    public void Remove<T>(string name) where T : notnull
    {
        if (resources[name] is IGraphicResource resource)
        {
            resource.Dispose();
        }

        resources.Remove(name);
    }
    
    /// <summary>
    /// Dispose all the resource, this will call the Dispose method of the resource if it's an IResource.
    /// </summary>
    public void Dispose()
    {
        foreach (KeyValuePair<string, object> item in resources)
        {
            if (item.Value is IGraphicResource resource)
            {
                resource.Dispose();
            }
        }
    }
}
