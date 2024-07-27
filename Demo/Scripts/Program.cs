using Demo.Scripts.Core;
using Foster.Framework;

namespace Demo.Scripts;

internal class Program
{
    static void Main(string[] args)
    {
        App.Register<Game>();
        App.Run(applicationName: "Foster Extended Shooter", width: 960, height: 540);
    }
}
