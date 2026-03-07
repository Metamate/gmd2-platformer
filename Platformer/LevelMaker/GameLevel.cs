using System.Linq;
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
    
    private readonly List<IEntity> _pendingAdd = [];
    private readonly List<IEntity> _pendingRemove = [];

    public void AddEntity(IEntity entity)
    {
        _pendingAdd.Add(entity);
    }

    public void RemoveEntity(IEntity entity)
    {
        _pendingRemove.Add(entity);
    }

    public void RandomizeGraphics(LevelMakerBase maker)
    {
        Tilemap.Tileset = maker.GetRandomTileset();
        Tilemap.Topperset = maker.GetRandomTopperset();
        Background = maker.GetRandomBackground();

        foreach (var bush in Entities.OfType<Bush>())
        {
            bush.Region = maker.GetRandomBush();
        }

        foreach (var box in Entities.OfType<MysteryBox>())
        {
            box.Region = maker.GetRandomMysteryBox();
        }
    }

    public void Update(GameTime gameTime)
    {
        Player?.Update(gameTime);
        foreach (var entity in Entities)
        {
            if (entity.Active)
            {
                entity.Update(gameTime);
                
                // Check interactions for all entities
                if (Player != null)
                {
                    entity.Collides(Player);
                }
            }
            else
            {
                RemoveEntity(entity);
            }
        }

        // Process deferred additions and removals
        foreach (var entity in _pendingRemove) Entities.Remove(entity);
        _pendingRemove.Clear();

        foreach (var entity in _pendingAdd) Entities.Add(entity);
        _pendingAdd.Clear();

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (Player == null) return;

        float worldWidth = Tilemap.Columns * Tilemap.TileWidth;
        float playerCenterX = Player.Position.X + (Player.Sprite.Width / 2f);
        float clampedX = MathHelper.Clamp(playerCenterX, GameSettings.VirtualWidth / 2f, worldWidth - GameSettings.VirtualWidth / 2f);

        Camera.Follow(
            new Vector2(clampedX, GameSettings.VirtualHeight / 2f),
            GameSettings.VirtualWidth,
            GameSettings.VirtualHeight
        );
    }

    public void Draw(SpriteBatch spriteBatch, Matrix screenScale)
    {
        DrawBackground(spriteBatch, screenScale);

        spriteBatch.Begin(transformMatrix: Camera.Transform * screenScale, samplerState: SamplerState.PointClamp);
        Tilemap.Draw(spriteBatch);

        foreach (var entity in Entities)
        {
            if (entity.Active)
            {
                entity.Draw(spriteBatch);
            }
        }

        Player?.Draw(spriteBatch);
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
