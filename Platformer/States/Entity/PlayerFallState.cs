using Microsoft.Xna.Framework;

namespace Platformer.States.Entity;

public class PlayerFallState(Player player) : PlayerStateBase(player)
{
    public override void Enter()
    {
        SetAnimation("fall-animation");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (IsOnGround())
        {
            if (Player.Velocity.X == 0)
                Player.ChangeState(new PlayerIdleState(Player));
            else
                Player.ChangeState(new PlayerWalkState(Player));
        }
    }
}