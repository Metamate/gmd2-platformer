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
        base.Update(gameTime);

        if (Player.Velocity.X == 0)
        {
            Player.ChangeState(new PlayerIdleState(Player));
        }

        if (GameController.Down)
        {
            Player.ChangeState(new PlayerDuckState(Player));
        }
    }
}