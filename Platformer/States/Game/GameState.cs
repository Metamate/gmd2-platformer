using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Game;

public abstract class GameState
{
    protected Game1 Game { get; }

    protected GameState(Game1 game)
    {
        Game = game;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}
