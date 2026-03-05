using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerDuckState(Player player) : PlayerState(player)
{
    public override void Enter() 
    { 
        SetAnimation("duck-animation");
        base.Enter();
    }

    protected override void HandleHorizontalMovement(GameTime gameTime)
    {
        // Shuts off horizontal movement by not calling base
        // But we still allow flipping
        if (GameController.Right) Sprite.Effects = SpriteEffects.None;
        else if (GameController.Left) Sprite.Effects = SpriteEffects.FlipHorizontally;

        Player.Velocity = new Vector2(0, Player.Velocity.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!GameController.Down)
        {
            Player.ChangeState(new PlayerIdleState(Player));
        }
    }
}
