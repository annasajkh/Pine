using Foster.Framework;
using PineSandbox.Scripts.Core;

namespace PineSandbox.Scripts;

internal class Program
{
    static void Main(string[] args)
    {
        App.Register<Application>();
        App.Run("PineSandbox", 960, 540);
    }
}