using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMDCore.Graphics;

public class Tile(int id, TextureRegion textureRegion, bool isCollidable, TextureRegion topper = null)
{
    public int Id { get; } = id;
    public TextureRegion Image { get; } = textureRegion;
    public bool IsCollidable { get; set; } = isCollidable;
    public TextureRegion Topper { get; set; } = topper;
    public int Width => Image.Width;
    public int Height => Image.Height;

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        Draw(spriteBatch, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        Image.Draw(spriteBatch, position, color, rotation, origin, scale, effects, layerDepth);
        Topper?.Draw(spriteBatch, position, color, rotation, origin, scale, effects, layerDepth);
    }
}