using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.States.Entity;

public class PlayerFallState(Player player) : PlayerState(player)
{
    public override void Enter()
    {
        // Define fall animation here when ready
        // _sprite = ...
        base.Enter();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // Add gravity and falling logic here
    }
}