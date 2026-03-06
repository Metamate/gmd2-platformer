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
        var animation = Player.Atlas.GetAnimation(name);
        if (Player.Sprite == null)
            Player.Sprite = new AnimatedSprite(animation);
        else
            Player.Sprite.Play(animation);
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
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
        float groundY = GetGroundY();
        if (Player.Position.Y >= groundY)
        {
            Player.Position = new Vector2(Player.Position.X, groundY);
            Player.Velocity = new Vector2(Player.Velocity.X, 0);
        }

        Player.Sprite?.Update(gameTime);
    }

    protected float GetGroundY()
    {
        Vector2 groundTilePos = Player.Tilemap.TileToPoint(0, Player.Tilemap.Rows - 3);
        return groundTilePos.Y - (Player.Sprite?.Height ?? 0);
    }
 
    protected bool IsOnGround() => Player.Position.Y >= GetGroundY() - 1f;

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Player.Sprite.Draw(spriteBatch, Player.Position);
    }
}