using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer;

namespace Platformer.States.Entity;

public abstract class PlayerState
{
    protected Player Player { get; }
    protected AnimatedSprite Sprite { get; set; }

    protected const float MoveSpeed = 60f;

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
        if (Sprite != null)
            Sprite.Effects = SpriteEffects.None;
    }

    protected virtual void HandleHorizontalMovement(GameTime gameTime)
    {
        float horizontal = 0;
        if (GameController.Left) horizontal -= 1;
        if (GameController.Right) horizontal += 1;

        Player.Velocity = new Vector2(horizontal * MoveSpeed, Player.Velocity.Y);

        if (horizontal > 0) Sprite.Effects = SpriteEffects.None;
        else if (horizontal < 0) Sprite.Effects = SpriteEffects.FlipHorizontally;
    }
    
    public virtual void Update(GameTime gameTime)
    {
        HandleHorizontalMovement(gameTime);
        Player.Position += Player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Sprite.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, Player.Position);
    }
}