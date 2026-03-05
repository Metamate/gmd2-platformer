using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

    public Game1() : base("Platformer", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();
        _levelMaker = new SimpleLevelMaker(Content);
        _tilemap = _levelMaker.Generate(100, 10);
        _background = _levelMaker.Backgrounds[Random.Shared.Next(_levelMaker.Backgrounds.Count)];
        _inputHandler = new InputHandler(_levelMaker, _tilemap);
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix, samplerState: SamplerState.PointClamp);

        _background.Draw(SpriteBatch, Vector2.Zero, Color.White);
        _tilemap.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
