using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.LevelMaker;

public class PillarsLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override Tilemap Generate(int columns, int rows)
    {
        Tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], columns, rows, Toppersets[Random.Shared.Next(Toppersets.Count)]);

        int groundHeight = 3;
        int pillarHeight = 2;
        float pillarChance = 0.15f;

        for (int x = 0; x < columns; x++)
        {
            int currentHeight = groundHeight;

            // Chance to spawn a pillar on top of it (avoiding the spawn area)
            if (x > 5 && Random.Shared.NextDouble() < pillarChance)
            {
                currentHeight += pillarHeight;
            }

            CreateGroundColumn(x, currentHeight);
        }

        return Tilemap;
    }
}
