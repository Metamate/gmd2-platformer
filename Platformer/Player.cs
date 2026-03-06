using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.LevelMaker;
using Platformer.States.Entity;

namespace Platformer
{
    public class Player
    {
        private TextureAtlas _atlas;
        private PlayerStateBase _currentState;

        public TextureAtlas Atlas => _atlas;
        public Tilemap Tilemap { get; }
        public AnimatedSprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Rectangle Hitbox => new Rectangle(
            (int)Position.X + 1,
            (int)Position.Y,
            (int)Sprite.Width - 2,
            (int)Sprite.Height
        );

        public Player(TextureAtlas textureAtlas, Tilemap tilemap)
        {
            _atlas = textureAtlas;
            Tilemap = tilemap;
            ChangeState(new PlayerIdleState(this));
            
            // Spawn at column 3, on top of the ground (which occupies the bottom 3 rows)
            Vector2 spawnPoint = tilemap.TileToPoint(3, tilemap.Rows - 3);
            Position = new Vector2(spawnPoint.X, spawnPoint.Y - Sprite.Height);
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
