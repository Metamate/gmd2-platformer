using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.States.PlayerStates;

namespace Platformer.Entities;

public class MysteryBox(TextureRegion region, Vector2 position) : IEntity
{
    public bool Collidable { get; set; } = true;
    public bool IsSolid => true;
    public bool Active { get; set; } = true;
    public Vector2 Position { get; set; } = position;
    public TextureRegion Region { get; set; } = region;
    public bool WasHit { get; private set; }

    public Rectangle Bounds => new((int)Position.X, (int)Position.Y, Region.Width, Region.Height);

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Visual feedback when the box is "depleted"
        Color color = WasHit ? Color.Gray : Color.White;
        Region.Draw(spriteBatch, Position, color);
    }

    public bool Collides(IEntity other)
    {
        if (!Active || !Collidable || !other.Collidable) return false;

        // Use a 1-pixel sensor to bridge the gap between "touching" and "actual intersection"
        Rectangle sensor = Bounds;
        sensor.Height += 1;

        bool isSensorTouching = sensor.Intersects(other.Bounds);

        if (isSensorTouching && !WasHit && other is Player player)
        {
            if (player.State is PlayerJumpState)
            {
                // Create a smaller "head" area to prevent hit-from-side triggers
                int horizontalInset = 1;
                Rectangle headArea = player.Bounds;
                headArea.X += horizontalInset;
                headArea.Width -= horizontalInset * 2;

                if (sensor.Intersects(headArea) && player.Bounds.Top >= Bounds.Bottom)
                {
                    OnHit();
                }
            }
        }

        return isSensorTouching;
    }

    private void OnHit()
    {
        WasHit = true;
        // TODO: Spawn a gem or play a sound here
    }
}
