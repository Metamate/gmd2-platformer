using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Input;

namespace Platformer.States.Entity;

public abstract class PlayerStateBase
{
    protected Player Player { get; }

    protected const float MoveSpeed = 100f;
    protected const float Gravity = 1000f;
    protected const int GroundInset = 4;

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

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Move X
        Player.Position = new Vector2(Player.Position.X + Player.Velocity.X * dt, Player.Position.Y);
        CheckLevelCollisionX();

        // Move Y
        Player.Position = new Vector2(Player.Position.X, Player.Position.Y + Player.Velocity.Y * dt);
        CheckLevelCollisionY();

        Player.Sprite?.Update(gameTime);
    }

    private void CheckLevelCollisionX()
    {
        Rectangle hitbox = Player.Hitbox;
        float tilemapWidth = Player.Tilemap.Columns * Player.Tilemap.TileWidth;

        // 1. World Boundary Checks
        if (Player.Position.X < 0)
        {
            Player.Position = new Vector2(0, Player.Position.Y);
            Player.Velocity = new Vector2(0, Player.Velocity.Y);
            return;
        }
        else if (Player.Position.X + Player.Sprite.Width > tilemapWidth)
        {
            Player.Position = new Vector2(tilemapWidth - Player.Sprite.Width, Player.Position.Y);
            Player.Velocity = new Vector2(0, Player.Velocity.Y);
            return;
        }

        // 2. Tilemap Collision Checks
        if (Player.Velocity.X > 0) // Moving Right
        {
            if (Player.Tilemap.IsSolidAt(hitbox.Right, hitbox.Top + 2) ||
                Player.Tilemap.IsSolidAt(hitbox.Right, hitbox.Bottom - 2))
            {
                int tileX = (int)(hitbox.Right / Player.Tilemap.TileWidth);
                Player.Position = new Vector2(tileX * Player.Tilemap.TileWidth - (Player.Sprite.Width - 1), Player.Position.Y);
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
            }
        }
        else if (Player.Velocity.X < 0) // Moving Left
        {
            if (Player.Tilemap.IsSolidAt(hitbox.Left, hitbox.Top + 2) ||
                Player.Tilemap.IsSolidAt(hitbox.Left, hitbox.Bottom - 2))
            {
                int tileX = (int)(hitbox.Left / Player.Tilemap.TileWidth);
                Player.Position = new Vector2((tileX + 1) * Player.Tilemap.TileWidth - 1, Player.Position.Y);
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
            }
        }
    }

    private void CheckLevelCollisionY()
    {
        Rectangle hitbox = Player.Hitbox;

        if (Player.Velocity.Y > 0) // Moving Down (Falling)
        {
            if (Player.Tilemap.IsSolidAt(hitbox.Left + GroundInset, hitbox.Bottom) ||
                Player.Tilemap.IsSolidAt(hitbox.Right - GroundInset, hitbox.Bottom))
            {
                int tileY = (int)(hitbox.Bottom / Player.Tilemap.TileHeight);
                Player.Position = new Vector2(Player.Position.X, tileY * Player.Tilemap.TileHeight - Player.Sprite.Height);
                Player.Velocity = new Vector2(Player.Velocity.X, 0);
            }
        }
        else if (Player.Velocity.Y < 0) // Moving Up (Jumping)
        {
            if (Player.Tilemap.IsSolidAt(hitbox.Left + GroundInset, hitbox.Top) ||
                Player.Tilemap.IsSolidAt(hitbox.Right - GroundInset, hitbox.Top))
            {
                int tileY = (int)(hitbox.Top / Player.Tilemap.TileHeight);
                Player.Position = new Vector2(Player.Position.X, (tileY + 1) * Player.Tilemap.TileHeight);
                Player.Velocity = new Vector2(Player.Velocity.X, 0);
            }
        }
    }

    protected bool IsOnGround()
    {
        Rectangle hitbox = Player.Hitbox;
        return Player.Tilemap.IsSolidAt(hitbox.Left + GroundInset, hitbox.Bottom + 1) ||
               Player.Tilemap.IsSolidAt(hitbox.Right - GroundInset, hitbox.Bottom + 1);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Player.Sprite.Draw(spriteBatch, Player.Position);
    }
}