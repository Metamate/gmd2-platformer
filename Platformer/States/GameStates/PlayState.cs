using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer.States.GameStates;

public class PlayState(Game1 game) : GameState(game)
{
    private InputHandler _inputHandler;
    private LevelMakerBase _levelMaker;
    private Entities.Player _player;
    private GameLevel _currentLevel;

    public override void Enter()
    {
        _levelMaker = new ComplexLevelMaker(Game.Content);
        _currentLevel = _levelMaker.Generate(30, 9);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Game.Content, "images/alien.xml");
        _player = new Entities.Player(alienAtlas, _currentLevel);
        _currentLevel.Player = _player;

        _inputHandler = new InputHandler(_levelMaker, _currentLevel);
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
