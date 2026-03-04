namespace GMDCore.Graphics;

public class Tile(int id, TextureRegion textureRegion, bool isTopper, bool isCollidable)
{
    public int Id { get; } = id;
    public TextureRegion Image { get; } = textureRegion;
    public bool IsTopper { get; set; } = isTopper;
    public bool IsCollidable { get; set; } = isCollidable;
}