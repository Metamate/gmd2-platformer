using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.LevelMaker;

public class SimpleLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override Tilemap Generate(int width, int height)
    {
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