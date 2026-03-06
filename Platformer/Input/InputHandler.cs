using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Platformer.LevelMaker;

namespace Platformer.Input;

public class InputHandler(LevelMakerBase levelMaker, Game1 game1)
{
    public void HandleInput()
    {
        if (GameController.Randomize)
        {
            game1.Tilemap.Tileset = levelMaker.GetRandomTileset();
            game1.Tilemap.Topperset = levelMaker.GetRandomTopperset();
            game1.Background = levelMaker.GetRandomBackground();
        }
    }
}