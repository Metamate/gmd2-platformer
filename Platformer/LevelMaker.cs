using System;
using System.Collections.Generic;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer;

public class LevelMaker
{
    private const int TileSize = 16;

    private const int TilesetWidth = 5;
    private const int TilesetHeight = 4;

    private const int TilesetsWide = 6;
    private const int TilesetsTall = 10;

    private const int ToppersetsWide = 6;
    private const int ToppersetsTall = 18;

    private const int TotalToppersets = ToppersetsWide * ToppersetsTall;
    private const int TotalTilesets = TilesetsWide * TilesetsTall;

    public List<Tileset> Tilesets = [];

    public Tilemap Generate(int width, int height, ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("images/tiles");

        for (int i = 0; i < TotalTilesets; i++)
        {
            int x = i % TilesetsWide * TilesetWidth * TileSize;
            int y = i / TilesetsTall * TilesetHeight * TileSize;

            TextureRegion textureRegion = new(texture, x, y, TilesetWidth * TileSize, TilesetHeight * TileSize);
            Tileset tileset = new(textureRegion, TileSize, TileSize);
            Tilesets.Add(tileset);
        }

        Tilemap tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], width, height);

        for (int i = 0; i < tilemap.Count; i++)
        {
            int x = i % tilemap.Columns;
            int y = i / tilemap.Columns;

            tilemap.SetTile(x, y, 0);
        }

        return tilemap;
    }
}