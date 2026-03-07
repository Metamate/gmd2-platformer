using GMDCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.States.GameStates;

namespace Platformer;

public class Game1 : Core
{
    private GameStateBase _currentState;
    public new Matrix ScreenScaleMatrix => base.ScreenScaleMatrix;

    public Game1() : base("Platformer", 1280, 720, GameSettings.VirtualWidth, GameSettings.VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();
        SetState(new StartState(this));
    }

    public void SetState(GameStateBase newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    protected override void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _currentState?.Draw(SpriteBatch);
        base.Draw(gameTime);
    }
}
