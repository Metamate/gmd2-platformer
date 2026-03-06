using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.LevelMaker;

public class SimpleLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override Tilemap Generate(int columns, int rows)
    {
        Tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], columns, rows);

        for (int i = 0; i < Tilemap.Count; i++)
        {
            int x = i % columns;
            int y = i / columns;

            Tilemap.SetTile(x, y, new Tile(0, false));
        }

        return Tilemap;
    }
}