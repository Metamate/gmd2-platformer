using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Content;
using Platformer.LevelMaker;

public class FlatLevelMaker(ContentManager content) : LevelMakerBase(content)
{
    public override Tilemap Generate(int columns, int rows)
    {
        Tilemap tilemap = new(Tilesets[Random.Shared.Next(Tilesets.Count)], columns, rows, Toppersets[Random.Shared.Next(Toppersets.Count)]);

        int groundHeight = 3;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Is this tile part of the ground? (i.e. is it in the bottom `groundHeight` rows?)
                if (y >= rows - groundHeight)
                {
                    // If it's the very top row of the ground, we give it a topper
                    if (y == rows - groundHeight)
                    {
                        tilemap.SetTile(x, y, new Tile(0, 0, true)); // Using TopperId 0 as an example
                    }
                    else
                    {
                        // Otherwise it's deep underground, just draw the standard solid tile block
                        tilemap.SetTile(x, y, new Tile(0, true));
                    }
                }
                else
                {
                    // Empty sky space, do nothing
                }
            }
        }

        return tilemap;
    }
}