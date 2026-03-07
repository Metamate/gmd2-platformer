using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Entities;

public class MysteryBox(TextureRegion region, Vector2 position) : IEntity
{
    public bool Collidable { get; set; } = true;
    public bool Active { get; set; } = true;
    public Vector2 Position { get; set; } = position;
    public TextureRegion Region { get; set; } = region;
    public bool WasHit { get; private set; }

    public Rectangle Bounds => new((int)Position.X, (int)Position.Y, Region.Width, Region.Height);

    public void Update(GameTime gameTime)
    {
        // Boxes are currently static environmental objects
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

        bool intersects = Bounds.Intersects(other.Bounds);
        
        // Classic "hit from below" interaction logic
        if (intersects && !WasHit && other is Player player)
        {
            // If player is moving UP and their head is below the box's center
            if (player.Velocity.Y < 0 && player.Position.Y > Position.Y)
            {
                OnHit();
            }
        }

        return intersects;
    }

    private void OnHit()
    {
        WasHit = true;
        // TODO: Spawn a gem or play a sound here later
    }
}
