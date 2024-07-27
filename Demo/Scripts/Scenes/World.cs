using Demo.Scripts.GameObjects;
using Demo.Scripts.GameObjects.Entities;
using Demo.Scripts.GameObjects.Entities.Bullets;
using Foster.Extended.Components;
using Foster.Extended.Managers;
using Foster.Framework;
using Vector2 = System.Numerics.Vector2;

namespace Demo.Scripts.Scenes;

public class World : Scene
{
    public ResourceManager ResourceManager { get; } = new();

    List<GameObject> gameObjects = new();

    Player player;

    public override void Startup()
    {
        ResourceManager.Add("player_texture", new Texture(new Image(Path.Combine("Assets", "Sprites", "SpriteSheets", "ship.png"))));
        ResourceManager.Add("laser_bolts", new Texture(new Image(Path.Combine("Assets", "Sprites", "SpriteSheets", "laser-bolts.png"))));

        player = new Player(new Vector2(App.Width / 2, App.Height / 2), this);
        player.FireBullet += () =>
        {
            gameObjects.Add(new PlayerLaserBolt(player.position, this));
        };
        
        gameObjects.Add(player);

    }

    public override void Update()
    {
        player.Update();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject is not Player)
            {
                gameObject.Update();
            }
        }
    }

    public override void Render(Batcher batcher)
    {
        batcher.PushMatrix(player.Camera.ViewMatrix);

        foreach (var gameObject in gameObjects)
        {
            if (gameObject is not Player)
            {
                gameObject.Render(batcher);
            }
        }

        player.Render(batcher);

        batcher.PopMatrix();
    }

    public override void Shutdown()
    {
        ResourceManager.Dispose();
    }
}
