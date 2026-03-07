using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Entities;

public class Bush(TextureRegion region, Vector2 position) : IEntity
{
    public bool Collidable { get; set; } = false;
    public bool Active { get; set; } = true;
    public Vector2 Position { get; set; } = position;
    public TextureRegion Region { get; set; } = region;

    public Rectangle Bounds => new((int)Position.X, (int)Position.Y, Region.Width, Region.Height);

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Region.Draw(spriteBatch, Position, Color.White);
    }
}
