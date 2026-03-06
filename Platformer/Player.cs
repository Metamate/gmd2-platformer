using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.States.Entity;

namespace Platformer
{
    public class Player
    {
        public const int HitboxInset = 1;

        private TextureAtlas _atlas;
        private PlayerStateBase _currentState;

        public TextureAtlas Atlas => _atlas;
        public Tilemap Tilemap { get; }
        public AnimatedSprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Rectangle Hitbox => new(
            (int)Position.X + HitboxInset,
            (int)Position.Y,
            (int)Sprite.Width - (HitboxInset * 2),
            (int)Sprite.Height
        );

        public Player(TextureAtlas textureAtlas, Tilemap tilemap)
        {
            _atlas = textureAtlas;
            Tilemap = tilemap;
            ChangeState(new PlayerIdleState(this));

            // Spawn at column 3, in the first row
            Position = tilemap.TileToPoint(3, 0);
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
}
