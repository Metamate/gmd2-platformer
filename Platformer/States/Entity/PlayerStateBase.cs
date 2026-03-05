using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer;

namespace Platformer.States.Entity;

public abstract class PlayerStateBase
{
    protected Player Player { get; }

    protected const float MoveSpeed = 100f;
    protected const float Gravity = 700f;

    protected PlayerStateBase(Player player)
    {
        Player = player;
    }

    protected void SetAnimation(string name)
    {
        Player.Sprite = Player.Atlas.CreateAnimatedSprite(name);
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
        if (Player.Sprite != null)
            Player.Sprite.Effects = SpriteEffects.None;
    }

    protected virtual void HandleHorizontalMovement(GameTime gameTime)
    {
        float horizontal = 0;
        if (GameController.Left) horizontal -= 1;
        if (GameController.Right) horizontal += 1;

        Player.Velocity = new Vector2(horizontal * MoveSpeed, Player.Velocity.Y);

        if (horizontal > 0) Player.Sprite.Effects = SpriteEffects.None;
        else if (horizontal < 0) Player.Sprite.Effects = SpriteEffects.FlipHorizontally;
    }

    protected virtual void ApplyGravity(GameTime gameTime)
    {
        Player.Velocity = new Vector2(
            Player.Velocity.X,
            Player.Velocity.Y + Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds
        );
    }

    public virtual void Update(GameTime gameTime)
    {
        HandleHorizontalMovement(gameTime);
        ApplyGravity(gameTime);

        Player.Position += Player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Temporary Floor Clamp (until we have real collision)
        float groundY = Game1.VirtualHeight - (Platformer.LevelMaker.LevelMakerBase.TileSize * 3 + Player.Sprite.Height);
        if (Player.Position.Y >= groundY)
        {
            Player.Position = new Vector2(Player.Position.X, groundY);
            Player.Velocity = new Vector2(Player.Velocity.X, 0);

            // Handle landing transition
            if (Player.Velocity.X == 0 && this is not PlayerIdleState && this is not PlayerDuckState)
                Player.ChangeState(new PlayerIdleState(Player));
            else if (Player.Velocity.X != 0 && this is not PlayerWalkState)
                Player.ChangeState(new PlayerWalkState(Player));
        }

        Player.Sprite?.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Player.Sprite.Draw(spriteBatch, Player.Position);
    }
}