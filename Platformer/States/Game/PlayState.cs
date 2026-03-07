using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Input;
using Platformer.LevelMaker;

namespace Platformer.States.Game;

public class PlayState(Game1 game) : GameState(game)
{
    private InputHandler _inputHandler;
    private LevelMakerBase _levelMaker;
    private Player _player;
    private GameLevel _currentLevel;

    public override void Enter()
    {
        _levelMaker = new ComplexLevelMaker(Game.Content);
        _currentLevel = _levelMaker.Generate(30, 9);

        TextureAtlas alienAtlas = TextureAtlas.FromFile(Game.Content, "images/alien.xml");
        _player = new Player(alienAtlas, _currentLevel);

        _inputHandler = new InputHandler(_levelMaker, _currentLevel);
    }

    public override void Update(GameTime gameTime)
    {
        _inputHandler.HandleInput();
        _player.Update(gameTime);
        _currentLevel.Update(gameTime);

        // Camera Follow Logic
        float worldWidth = _currentLevel.Tilemap.Columns * _currentLevel.Tilemap.TileWidth;
        float playerCenterX = _player.Position.X + (_player.Sprite.Width / 2f);
        float clampedX = MathHelper.Clamp(playerCenterX, GameSettings.VirtualWidth / 2f, worldWidth - GameSettings.VirtualWidth / 2f);
        _currentLevel.Camera.Follow(new Vector2(clampedX, GameSettings.VirtualHeight / 2f), GameSettings.VirtualWidth, GameSettings.VirtualHeight);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _currentLevel.Draw(spriteBatch, Game.ScreenScaleMatrix);

        // Draw Player in world space
        spriteBatch.Begin(transformMatrix: _currentLevel.Camera.Transform * Game.ScreenScaleMatrix, samplerState: SamplerState.PointClamp);
        _player.Draw(spriteBatch);
        spriteBatch.End();
    }
}
