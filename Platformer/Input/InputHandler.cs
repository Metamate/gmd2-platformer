using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Platformer.LevelMaker;

namespace Platformer.Input;

public class InputHandler(LevelMakerBase levelMaker, Tilemap tilemap, TextureRegion background)
{
    public void HandleInput()
    {
        if (GameController.Randomize)
        {
            tilemap.Tileset = levelMaker.GetRandomTileset();
            tilemap.Topperset = levelMaker.GetRandomTopperset();
            background.SetRegion(levelMaker.GetRandomBackground());
        }
    }
}