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
    public Player Player { get; set; }
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

        if (Player != null)
        {
            float worldWidth = Tilemap.Columns * Tilemap.TileWidth;
            float playerCenterX = Player.Position.X + (Player.Sprite.Width / 2f);
            float clampedX = MathHelper.Clamp(playerCenterX, GameSettings.VirtualWidth / 2f, worldWidth - GameSettings.VirtualWidth / 2f);
            
            Camera.Follow(new Vector2(clampedX, GameSettings.VirtualHeight / 2f), GameSettings.VirtualWidth, GameSettings.VirtualHeight);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Matrix screenScale)
    {
        // Draw Background
        float parallaxFactor = 0.5f;
        float bgOffset = -(Camera.Position.X * parallaxFactor) % Background.Width;

        spriteBatch.Begin(transformMatrix: screenScale, samplerState: SamplerState.PointClamp);
        Background.Draw(spriteBatch, new Vector2(bgOffset, 0), Color.White);
        Background.Draw(spriteBatch, new Vector2(bgOffset + Background.Width, 0), Color.White);
        spriteBatch.End();

        // Draw Tilemap
        spriteBatch.Begin(transformMatrix: Camera.Transform * screenScale, samplerState: SamplerState.PointClamp);
        Tilemap.Draw(spriteBatch);
        
        // Draw Entities
        foreach (var entity in Entities)
        {
            entity.Draw(spriteBatch);
        }
        spriteBatch.End();
    }
}
