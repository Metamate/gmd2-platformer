using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer3.Entities;

namespace Platformer3.LevelMaker;

public class GameLevel(Tilemap tilemap, TextureRegion background)
{
    public Tilemap Tilemap { get; } = tilemap;
    public TextureRegion Background { get; set; } = background;
    public Player Player { get; set; }

    public void RandomizeGraphics(LevelMakerBase maker)
    {
        Tilemap.Tileset = maker.GetRandomTileset();
        Tilemap.Topperset = maker.GetRandomTopperset();
        Background = maker.GetRandomBackground();
    }

    public void Update(GameTime gameTime)
    {
        Player?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, Matrix screenScale)
    {
        spriteBatch.Begin(transformMatrix: screenScale, samplerState: SamplerState.PointClamp);
        Background.Draw(spriteBatch, Vector2.Zero, Color.White);
        Tilemap.Draw(spriteBatch);
        Player?.Draw(spriteBatch);
        spriteBatch.End();
    }
}
