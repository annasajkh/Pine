﻿using Foster.Framework;
using PineSandbox.Scripts.Core;

namespace PineSandbox.Scripts;

internal class Program
{
    static void Main(string[] args)
    {
        if (OperatingSystem.IsWindows())
        {
            new Application("Pine Sandbox", 960, 540, GraphicsDriver.D3D12).Run();
        }
        else if (OperatingSystem.IsLinux())
        {
            new Application("Pine Sandbox", 960, 540, GraphicsDriver.Vulkan).Run();
        }
        else if (OperatingSystem.IsMacOS())
        {
            new Application("Pine Sandbox", 960, 540, GraphicsDriver.Metal).Run();
        }
    }
}