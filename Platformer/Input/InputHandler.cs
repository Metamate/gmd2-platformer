using Platformer.LevelMaker;

namespace Platformer.Input;

public class InputHandler(LevelMakerBase levelMaker, GameLevel currentLevel)
{
    public void HandleInput()
    {
        if (GameController.Randomize)
        {
            currentLevel.RandomizeGraphics(levelMaker);
        }
    }
}