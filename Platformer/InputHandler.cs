using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class InputHandler(LevelMaker levelMaker, Tilemap tilemap)
    {
        public void HandleInput()
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
            {
                tilemap.Tileset = levelMaker.Tilesets[Random.Shared.Next(levelMaker.Tilesets.Count)];
            }
        }
    }
}