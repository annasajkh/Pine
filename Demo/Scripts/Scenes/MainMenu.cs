using Demo.Scripts.Core;
using Foster.Extended.Components;
using Foster.Extended.Extensions;
using Foster.Framework;
using System.Numerics;

namespace Demo.Scripts.Scenes;

public class MainMenu : Scene
{
    public override void Startup()
    {

    }

    public override void Update()
    {
        if (Input.Keyboard.Pressed(Keys.Enter))
        {
            Game.SceneManager.ChangeScene("World");
        }
    }

    public override void Render(Batcher batcher)
    {
        batcher.TextCentered("Press Enter To Start", new Vector2(App.Width / 2, App.Height / 2), Color.White, Game.ResourceManager.Get<SpriteFont>("ArialFont32"));
    }

    public override void Shutdown()
    {

    }
}