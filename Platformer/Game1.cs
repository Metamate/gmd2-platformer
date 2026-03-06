using System;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer;

public class Game1 : Core
{
    public const int VirtualWidth = 256;
    public const int VirtualHeight = 144;
    private InputHandler _inputHandler;
    public Tilemap Tilemap { get; set; }
    public TextureRegion Background { get; set; }
    private LevelMakerBase _levelMaker;
    private Player _player;
    private Camera _camera;

    public Game1() : base("Platformer", 1280, 720, VirtualWidth, VirtualHeight)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();
        _levelMaker = new ComplexLevelMaker(Content);
        Tilemap = _levelMaker.Generate(30, 9);
        Background = _levelMaker.GetRandomBackground();
        _inputHandler = new InputHandler(_levelMaker, this);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Content, "images/alien.xml");
        _player = new Player(alienAtlas, Tilemap);
        _camera = new Camera();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _player.Update(gameTime);

        float worldWidth = Tilemap.Columns * Tilemap.TileWidth;
        float playerCenterX = _player.Position.X + (_player.Sprite.Width / 2f);
        float clampedX = MathHelper.Clamp(playerCenterX, VirtualWidth / 2f, worldWidth - VirtualWidth / 2f);
        _camera.Follow(new Vector2(clampedX, VirtualHeight / 2f), VirtualWidth, VirtualHeight);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Draw Background
        float parallaxFactor = 0.5f;
        float bgOffset = -(_camera.Position.X * parallaxFactor) % Background.Width;

        SpriteBatch.Begin(transformMatrix: ScreenScaleMatrix, samplerState: SamplerState.PointClamp);

        Background.Draw(SpriteBatch, new Vector2(bgOffset, 0), Color.White);
        Background.Draw(SpriteBatch, new Vector2(bgOffset + Background.Width, 0), Color.White);

        SpriteBatch.End();

        // Draw World
        SpriteBatch.Begin(transformMatrix: _camera.Transform * ScreenScaleMatrix, samplerState: SamplerState.PointClamp);

        Tilemap.Draw(SpriteBatch);
        _player.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
