using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer;

public class Game1 : Core
{
    public const int VirtualWidth = 256;
    public const int VirtualHeight = 144;
    private InputHandler _inputHandler;
    private LevelMakerBase _levelMaker;
    private Tilemap _tilemap;
    private TextureRegion _background;
    private Player _player;

    public Game1() : base("Platformer", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();
        _levelMaker = new FlatLevelMaker(Content);
        _tilemap = _levelMaker.Generate(100, 9);
        _background = _levelMaker.GetRandomBackground();
        _inputHandler = new InputHandler(_levelMaker, _tilemap, _background);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Content, "images/alien.xml");
        _player = new Player(alienAtlas);
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _player.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix, samplerState: SamplerState.PointClamp);

        _background.Draw(SpriteBatch, Vector2.Zero, Color.White);
        _tilemap.Draw(SpriteBatch);
        _player.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
