using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerIdleState(Player player) : PlayerState(player)
{
    public override void Enter() 
    { 
        SetAnimation("idle-animation");
        base.Enter();
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