using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer;

namespace Platformer.States.Entity;

public abstract class PlayerState
{
    protected Player Player { get; }
    protected AnimatedSprite Sprite { get; set; }

    protected PlayerState(Player player)
    {
        Player = player;
    }

    protected void EnsureRegion(string name, int x, int y, int w, int h)
    {
        try { Player.Atlas.GetRegion(name); }
        catch { Player.Atlas.AddRegion(name, x, y, w, h); }
    }

    public virtual void Enter() 
    { 
        if (Sprite != null)
        {
            Player.Sprite = Sprite;
        }
    }

    public virtual void Exit()
    {
        Sprite.Effects = SpriteEffects.None;
    }
    
    public virtual void Update(GameTime gameTime)
    {
        Sprite.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Player.Position);
    }
}