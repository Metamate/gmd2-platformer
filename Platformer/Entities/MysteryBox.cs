using System;
using System.Collections.Generic;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.States.PlayerStates;
using Platformer.LevelMaker;

namespace Platformer.Entities;

public class MysteryBox(GameLevel level, TextureRegion region, Vector2 position, List<TextureRegion> gemPool) : IEntity
{
    private readonly List<TextureRegion> _gemPool = gemPool;
    public GameLevel Level { get; } = level;
    public bool Collidable { get; set; } = true;
    public bool IsSolid => true;
    public bool Active { get; set; } = true;
    public Vector2 Position { get; set; } = position;
    public TextureRegion Region { get; set; } = region;
    public bool WasHit { get; private set; }

    public Rectangle Bounds => new((int)Position.X, (int)Position.Y, Region.Width, Region.Height);

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Visual feedback when the box is "depleted"
        Color color = WasHit ? Color.Gray : Color.White;
        Region.Draw(spriteBatch, Position, color);
    }

    public bool Collides(IEntity other)
    {
        if (!Active || !Collidable || !other.Collidable) return false;

        // Use a 1-pixel sensor to bridge the gap between "touching" and "actual intersection"
        Rectangle sensor = Bounds;
        sensor.Height += 1;

        bool isSensorTouching = sensor.Intersects(other.Bounds);

        if (isSensorTouching && !WasHit && other is Player player)
        {
            if (player.State is PlayerJumpState)
            {
                // Create a smaller "head" area to prevent hit-from-side triggers
                int horizontalInset = 1;
                Rectangle headArea = player.Bounds;
                headArea.X += horizontalInset;
                headArea.Width -= horizontalInset * 2;

                if (sensor.Intersects(headArea) && player.Bounds.Top >= Bounds.Bottom)
                {
                    OnHit();
                }
            }
        }

        return isSensorTouching;
    }

    private void OnHit()
    {
        WasHit = true;

        if (_gemPool != null && _gemPool.Count > 0)
        {
            var gemRegion = _gemPool[Random.Shared.Next(_gemPool.Count)];
            // Spawn gem above the box with an upward pop (initial velocity -300f)
            Vector2 gemPos = new Vector2(Position.X, Position.Y - gemRegion.Height);
            Level.AddEntity(new Gem(gemRegion, gemPos, new Vector2(0, -300f)));
        }
    }
}
