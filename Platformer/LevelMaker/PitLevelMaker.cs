using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.LevelMaker;

public class PitLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override Tilemap Generate(int columns, int rows)
    {
        Tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], columns, rows, Toppersets[Random.Shared.Next(Toppersets.Count)]);

        int groundHeight = 3;
        float pitChance = 0.2f;

        for (int x = 0; x < columns; x++)
        {
            // Skip pit generation for the first few columns to ensure player spawns on ground
            if (x > 3 && Random.Shared.NextDouble() < pitChance)
            {
                continue;
            }

            CreateGroundColumn(x, groundHeight);
        }

        return Tilemap;
    }
}