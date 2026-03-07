using System.Collections.Generic;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Graphics;

namespace Platformer.LevelMaker;

public class GameLevel(Tilemap tilemap, TextureRegion background)
{
    public Tilemap Tilemap { get; } = tilemap;
    public TextureRegion Background { get; set; } = background;
    public Camera Camera { get; } = new();
    public List<IEntity> Entities { get; } = [];

    public void AddEntity(IEntity entity)
    {
        Entities.Add(entity);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Matrix screenScale)
    {
        DrawBackground(spriteBatch, screenScale);

        spriteBatch.Begin(transformMatrix: Camera.Transform * screenScale, samplerState: SamplerState.PointClamp);
        Tilemap.Draw(spriteBatch);

        foreach (var entity in Entities)
        {
            entity.Draw(spriteBatch);
        }
        spriteBatch.End();
    }

    public void DrawBackground(SpriteBatch spriteBatch, Matrix screenScale)
    {
        float parallaxFactor = 0.5f;
        float bgOffset = -(Camera.Position.X * parallaxFactor) % Background.Width;

        spriteBatch.Begin(transformMatrix: screenScale, samplerState: SamplerState.PointClamp);
        Background.Draw(spriteBatch, new Vector2(bgOffset, 0), Color.White);
        Background.Draw(spriteBatch, new Vector2(bgOffset + Background.Width, 0), Color.White);
        spriteBatch.End();
    }
}
