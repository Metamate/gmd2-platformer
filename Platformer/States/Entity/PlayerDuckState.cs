using System;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Entity;

public class PlayerDuckState : PlayerState
{
    public PlayerDuckState(Player player) : base(player)
    {
        // Define duck frame (assuming frame 10 for now)
        EnsureRegion("duck_frame", 160, 0, 16, 20);

        // Define duck animation
        try { Player.Atlas.GetAnimation("duck"); }
        catch 
        { 
            Player.Atlas.AddAnimation("duck", new Animation(
                [ Player.Atlas.GetRegion("duck_frame") ], 
                TimeSpan.FromMilliseconds(500)
            )); 
        }

        Sprite = Player.Atlas.CreateAnimatedSprite("duck");
    }

    protected override void HandleHorizontalMovement(GameTime gameTime)
    {
        // Shuts off horizontal movement by not calling base
        // But we still allow flipping
        bool left = GMDCore.Core.Input.Keyboard.IsKeyDown(Keys.Left);
        bool right = GMDCore.Core.Input.Keyboard.IsKeyDown(Keys.Right);

        if (right) Sprite.Effects = SpriteEffects.None;
        else if (left) Sprite.Effects = SpriteEffects.FlipHorizontally;

        Player.Velocity = new Vector2(0, Player.Velocity.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!GMDCore.Core.Input.Keyboard.IsKeyDown(Keys.Down))
        {
            Player.ChangeState(new PlayerIdleState(Player));
        }
    }
}
