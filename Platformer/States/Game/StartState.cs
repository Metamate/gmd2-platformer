using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.States.Game;

public class StartState : GameState
{
    private SpriteFont _font;
    private string _title = "VIA Mario Bros";
    private Vector2 _titlePosition;

    public StartState(Game1 game) : base(game)
    {
    }

    public override void Enter()
    {
        _font = Game.Content.Load<SpriteFont>("fonts/font");
        Vector2 size = _font.MeasureString(_title);
        _titlePosition = new Vector2(
            GameSettings.VirtualWidth / 2f - size.X / 2f,
            GameSettings.VirtualHeight / 2f - size.Y / 2f
        );
    }

    public override void Exit()
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            Game.SetState(new PlayState(Game));
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_font != null)
        {
            spriteBatch.Begin(transformMatrix: Game.ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
            spriteBatch.DrawString(_font, _title, _titlePosition, Color.White);
            spriteBatch.End();
        }
    }
}
