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
    private GameLevel _currentLevel;

    public PlayState(Game1 game) : base(game)
    {
    }

    public override void Enter()
    {
        _levelMaker = new ComplexLevelMaker(Game.Content);
        _currentLevel = _levelMaker.Generate(30, 9);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Game.Content, "images/alien.xml");
        _player = new Player(alienAtlas, _currentLevel);
        _currentLevel.Player = _player;

        _inputHandler = new InputHandler(_levelMaker, _currentLevel);
    }

    public override void Exit()
    {
    }

    public override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _currentLevel.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _currentLevel.Draw(spriteBatch, Game.ScreenScaleMatrix);
    }
}
