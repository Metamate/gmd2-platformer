using System;
using Microsoft.Xna.Framework.Content;
using Platformer.LevelMaker;

public class FlatLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override GameLevel Generate(int columns, int rows)
    {
        Tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], columns, rows, Toppersets[Random.Shared.Next(Toppersets.Count)]);

        int groundHeight = 3;

        for (int x = 0; x < columns; x++)
        {
            CreateGroundColumn(x, groundHeight);
        }

        return new GameLevel(Tilemap, GetRandomBackground());
    }
}