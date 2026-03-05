using Microsoft.Xna.Framework;

namespace Platformer.States.Entity;

public class PlayerWalkState(Player player) : PlayerStateBase(player)
{
    public override void Enter()
    {
        SetAnimation("walk-animation");
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

        if (GameController.Jump)
        {
            Player.ChangeState(new PlayerJumpState(Player));
        }

        if (Player.Velocity.Y > 0.1f)
        {
            Player.ChangeState(new PlayerFallState(Player));
        }
    }
}