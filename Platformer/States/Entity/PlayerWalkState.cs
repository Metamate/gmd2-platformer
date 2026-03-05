using System;
using System.Collections.Generic;
using System.Linq;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerWalkState(Player player) : PlayerState(player)
{
    private const float Speed = 60;

    public override void Enter() 
    { 
        SetAnimation("walk-animation");
        base.Enter();
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