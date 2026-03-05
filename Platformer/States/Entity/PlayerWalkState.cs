using System;
using System.Collections.Generic;
using System.Linq;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerWalkState : PlayerState
{
    private const float Speed = 60;

    public PlayerWalkState(Player player) : base(player)
    {
        // Define walk frames
        int frameWidth = 16;
        for (int i = 0; i < 9; i++)
        {
            EnsureRegion($"walk_{i}", i * frameWidth, 0, frameWidth, 20);
        }

        // Define animation
        try { Player.Atlas.GetAnimation("walk"); }
        catch 
        {
            var regions = new List<TextureRegion>();
            for (int i = 0; i < 9; i++) regions.Add(Player.Atlas.GetRegion($"walk_{i}"));
            
            Player.Atlas.AddAnimation("walk", new Animation(regions, TimeSpan.FromMilliseconds(100)));
        }

        Sprite = Player.Atlas.CreateAnimatedSprite("walk");
    }

    public override void Update(GameTime gameTime)
    {
        bool left = Core.Input.Keyboard.IsKeyDown(Keys.Left);
        bool right = Core.Input.Keyboard.IsKeyDown(Keys.Right);

        if (right)
        {
            Player.Position = new Vector2(Player.Position.X + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Player.Position.Y);
            Player.Sprite.Effects = SpriteEffects.None;
        }
        else if (left)
        {
            Player.Position = new Vector2(Player.Position.X - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Player.Position.Y);
            Player.Sprite.Effects = SpriteEffects.FlipHorizontally;
        }
        else
        {
            Player.ChangeState(new PlayerIdleState(Player));
        }
        base.Update(gameTime);
    }
}