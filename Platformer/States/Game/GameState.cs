using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Game;

public abstract class GameState(Game1 game)
{
    protected Game1 Game { get; } = game;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}
