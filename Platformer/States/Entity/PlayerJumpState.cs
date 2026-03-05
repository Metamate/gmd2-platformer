using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Entity;

public class PlayerJumpState(Player player) : PlayerStateBase(player)
{
    private const float JumpImpulse = -250f;

    public override void Enter()
    {
        SetAnimation("jump-animation");
        Player.Velocity = new Vector2(Player.Velocity.X, JumpImpulse);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Player.Velocity.Y > 0)
        {
            Player.ChangeState(new PlayerFallState(Player));
        }
    }
}