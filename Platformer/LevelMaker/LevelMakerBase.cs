using System;
using System.Collections.Generic;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.LevelMaker;

public abstract class LevelMakerBase
{
    private const int TileSize = 16;
    private const int TilesetsColumns = 6;
    private const int TilesetsRows = 10;

    private const int ToppersetsColumns = 6;
    private const int ToppersetsRows = 18;

    public List<Tileset> Tilesets { get; } = [];
    public List<Tileset> Toppersets { get; } = [];
    public List<TextureRegion> Backgrounds { get; } = [];
    protected Tilemap Tilemap { get; set; }

    private ContentManager _content;

    public LevelMakerBase(ContentManager content)
    {
        _content = content;
        Tilesets = CreateTilesetsFromFile("images/tiles", TilesetsColumns, TilesetsRows, TileSize);
        Toppersets = CreateTilesetsFromFile("images/tile_tops", ToppersetsColumns, ToppersetsRows, TileSize);
        Backgrounds = GetBackgroundsFromFile("images/backgrounds", 1, 3);
    }

    public abstract Tilemap Generate(int columns, int rows);

    protected void CreateGroundColumn(int x, int groundHeight)
    {
        for (int y = 0; y < Tilemap.Rows; y++)
        {
            if (y >= Tilemap.Rows - groundHeight)
            {
                if (y == Tilemap.Rows - groundHeight)
                {
                    // Top of ground with topper
                    Tilemap.SetTile(x, y, new Tile(0, 0, true));
                }
                else
                {
                    // Solid underground tile
                    Tilemap.SetTile(x, y, new Tile(0, true));
                }
            }
        }
    }

    private List<Tileset> CreateTilesetsFromFile(string file, int columns, int rows, int tileSize)
    {
        List<Tileset> tilesets = [];

        Texture2D texture = _content.Load<Texture2D>(file);

        int tilesetWidth = texture.Width / columns;
        int tilesetHeight = texture.Height / rows;
        int count = columns * rows;

        for (int i = 0; i < count; i++)
        {
            int x = i % columns * tilesetWidth;
            int y = i / columns * tilesetHeight;

            TextureRegion textureRegion = new(texture, x, y, tilesetWidth, tilesetHeight);
            Tileset tileset = new(textureRegion, tileSize, tileSize);
            tilesets.Add(tileset);
        }

        return tilesets;
    }

    public List<TextureRegion> GetBackgroundsFromFile(string file, int columns, int rows)
    {
        List<TextureRegion> backgrounds = [];

        Texture2D texture = _content.Load<Texture2D>(file);

        int backgroundWidth = texture.Width / columns;
        int backgroundHeight = texture.Height / rows;
        int count = columns * rows;

        for (int i = 0; i < count; i++)
        {
            int x = i % columns * backgroundWidth;
            int y = i / columns * backgroundHeight;

            TextureRegion textureRegion = new(texture, x, y, backgroundWidth, backgroundHeight);
            backgrounds.Add(textureRegion);
        }

        return backgrounds;
    }

    public Tileset GetRandomTileset()
    {
        return Tilesets[Random.Shared.Next(Tilesets.Count)];
    }

    public Tileset GetRandomTopperset()
    {
        return Toppersets[Random.Shared.Next(Toppersets.Count)];
    }

    public TextureRegion GetRandomBackground()
    {
        return Backgrounds[Random.Shared.Next(Backgrounds.Count)];
    }
}