using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.States.Entity;
using Platformer.LevelMaker;

namespace Platformer.Entities;

public class Player : IEntity
{
    public const int HitboxInset = 2;

    private TextureAtlas _atlas;
    private PlayerStateBase _currentState;

    public TextureAtlas Atlas => _atlas;
    public GameLevel Level { get; }
    public Tilemap Tilemap => Level.Tilemap;
    public AnimatedSprite Sprite { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float CoyoteTimer { get; set; }

    public Rectangle Hitbox => new(
        (int)Position.X + HitboxInset,
        (int)Position.Y,
        (int)Sprite.Width - (HitboxInset * 2),
        (int)Sprite.Height
    );

    public bool Collidable { get; set; } = true;

    public Player(TextureAtlas textureAtlas, GameLevel level)
    {
        _atlas = textureAtlas;
        Level = level;
        ChangeState(new PlayerIdleState(this));

        Position = Tilemap.TileToPoint(3, 0);
    }

    public void ChangeState(PlayerStateBase newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update(GameTime gameTime)
    {
        _currentState.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentState.Draw(spriteBatch);
    }
}
