using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.LevelMaker;

namespace Platformer
{
    public class InputHandler(LevelMakerBase levelMaker, Tilemap tilemap)
    {
        public void HandleInput()
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
            {
                tilemap.Tileset = levelMaker.GetRandomTileset();
            }
        }
    }
}