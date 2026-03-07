using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Entities;

public interface IEntity
{
    public bool Collidable { get; set; }
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
