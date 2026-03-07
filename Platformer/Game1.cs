using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Graphics;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer;

public class Game1 : Core
{
    private InputHandler _inputHandler;
    public GameLevel CurrentLevel { get; set; }
    private LevelMakerBase _levelMaker;
    private Player _player;

    public Game1() : base("Platformer", 1280, 720, GameSettings.VirtualWidth, GameSettings.VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();
        _levelMaker = new ComplexLevelMaker(Content);
        CurrentLevel = _levelMaker.Generate(30, 9);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Content, "images/alien.xml");
        _player = new Player(alienAtlas, CurrentLevel);
        CurrentLevel.Player = _player;

        _inputHandler = new InputHandler(_levelMaker, CurrentLevel);
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        CurrentLevel.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        CurrentLevel.Draw(SpriteBatch, ScreenScaleMatrix);

        base.Draw(gameTime);
    }
}
