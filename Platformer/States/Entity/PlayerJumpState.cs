using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Entity;

public class PlayerJumpState(Player player) : PlayerState(player)
{
    public override void Enter()
    {
        // Define jump animation here when ready
        // _sprite = ...
        base.Enter();
    }
}