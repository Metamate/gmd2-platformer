using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer.States.Game;

public class PlayState : GameState
{
    private InputHandler _inputHandler;
    private LevelMakerBase _levelMaker;
    private Player _player;
    public GameLevel CurrentLevel { get; private set; }

    public PlayState(Game1 game) : base(game)
    {
    }

    public override void Enter()
    {
        _levelMaker = new ComplexLevelMaker(Game.Content);
        CurrentLevel = _levelMaker.Generate(30, 9);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Game.Content, "images/alien.xml");
        _player = new Player(alienAtlas, CurrentLevel);
        CurrentLevel.Player = _player;

        _inputHandler = new InputHandler(_levelMaker, CurrentLevel);
    }

    public override void Exit()
    {
    }

    public override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        CurrentLevel.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        CurrentLevel.Draw(spriteBatch, Game.ScreenScaleMatrix);
    }
}
