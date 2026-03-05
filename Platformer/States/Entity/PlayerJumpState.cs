using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Entity;

public class PlayerJumpState(Player player) : PlayerState(player)
{
    public override void Enter()
    {
        SetAnimation("jump-animation");
        base.Enter();
    }
}