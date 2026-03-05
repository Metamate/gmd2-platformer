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
        public AnimatedSprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Player(TextureAtlas textureAtlas)
        {
            _atlas = textureAtlas;
            ChangeState(new PlayerIdleState(this));
            Position = new Vector2(LevelMakerBase.TileSize * 3, Game1.VirtualHeight - (LevelMakerBase.TileSize * 3 + Sprite.Height));
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
