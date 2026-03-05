using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player)
    {
        // Define frames if needed
        EnsureRegion("idle_frame", 0, 0, 16, 20);

        // Define animation if needed
        try { Player.Atlas.GetAnimation("idle"); }
        catch 
        { 
            Player.Atlas.AddAnimation("idle", new Animation(
                [ Player.Atlas.GetRegion("idle_frame") ], 
                TimeSpan.FromMilliseconds(500)
            )); 
        }

        Sprite = Player.Atlas.CreateAnimatedSprite("idle");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Player.Velocity.X != 0)
        {
            Player.ChangeState(new PlayerWalkState(Player));
        }
        
        if (GMDCore.Core.Input.Keyboard.IsKeyDown(Keys.Down))
        {
            Player.ChangeState(new PlayerDuckState(Player));
        }
    }
}