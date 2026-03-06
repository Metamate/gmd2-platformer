using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Input;

namespace Platformer.States.Entity;

public abstract class PlayerStateBase
{
    protected const float MoveSpeed = 100f;
    protected const float Gravity = 1000f;
    protected const int GroundInset = 4;

    protected Player Player { get; }

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

        // Resolve X (Move then Snap)
        Player.Position = new Vector2(Player.Position.X + Player.Velocity.X * dt, Player.Position.Y);
        ResolveXCollisions();

        // Resolve Y (Move then Snap)
        Player.Position = new Vector2(Player.Position.X, Player.Position.Y + Player.Velocity.Y * dt);
        ResolveYCollisions();

        Player.Sprite?.Update(gameTime);
    }

    private void ResolveXCollisions()
    {
        Rectangle hitbox = Player.Hitbox;
        float tilemapWidth = Player.Tilemap.Columns * Player.Tilemap.TileWidth;

        if (Player.Velocity.X > 0) // Moving Right
        {
            if (hitbox.Right > tilemapWidth) { SnapToRight(tilemapWidth); return; }
            if (CheckSideCollision(hitbox.Right)) SnapToRight(Player.Tilemap.GetTileLeft(hitbox.Right));
        }
        else if (Player.Velocity.X < 0) // Moving Left
        {
            if (hitbox.Left < 0) { SnapToLeft(0); return; }
            if (CheckSideCollision(hitbox.Left)) SnapToLeft(Player.Tilemap.GetTileRight(hitbox.Left));
        }
    }

    private void ResolveYCollisions()
    {
        Rectangle hitbox = Player.Hitbox;

        if (Player.Velocity.Y > 0) // Falling
        {
            if (CheckVerticalCollision(hitbox.Bottom))
            {
                SnapToBottom(Player.Tilemap.GetTileTop(hitbox.Bottom));
            }
        }
        else if (Player.Velocity.Y < 0) // Jumping
        {
            if (CheckVerticalCollision(hitbox.Top))
            {
                SnapToTop(Player.Tilemap.GetTileBottom(hitbox.Top));
            }
        }
    }

    private bool CheckSideCollision(float x)
    {
        // Check the top and bottom of the side-edge (slighthly inset)
        return Player.Tilemap.IsSolidAt(x, Player.Hitbox.Top + 2) ||
               Player.Tilemap.IsSolidAt(x, Player.Hitbox.Bottom - 2);
    }

    private bool CheckVerticalCollision(float y)
    {
        // Check left and right of the top/bottom edge (slightly inset using GroundInset)
        return Player.Tilemap.IsSolidAt(Player.Hitbox.Left + GroundInset, y) ||
               Player.Tilemap.IsSolidAt(Player.Hitbox.Right - GroundInset, y);
    }

    private void SnapToRight(float x)
    {
        Player.Position = new Vector2(x - Player.Sprite.Width + Player.HitboxInset, Player.Position.Y);
        Player.Velocity = new Vector2(0, Player.Velocity.Y);
    }

    private void SnapToLeft(float x)
    {
        Player.Position = new Vector2(x - Player.HitboxInset, Player.Position.Y);
        Player.Velocity = new Vector2(0, Player.Velocity.Y);
    }

    private void SnapToBottom(float y)
    {
        Player.Position = new Vector2(Player.Position.X, y - Player.Sprite.Height);
        Player.Velocity = new Vector2(Player.Velocity.X, 0);
    }

    private void SnapToTop(float y)
    {
        Player.Position = new Vector2(Player.Position.X, y);
        Player.Velocity = new Vector2(Player.Velocity.X, 0);
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