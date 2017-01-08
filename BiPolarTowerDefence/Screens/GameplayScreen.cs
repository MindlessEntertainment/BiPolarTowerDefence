using BiPolarTowerDefence.Entities;
using BiPolarTowerDefence.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Screens
{
    public class GameplayScreen : BaseScreen
    {
        private Level level;
        public bool PauseGame { get; private set; } = false;

        public override void Load()
        {
            level = new Level(Game1.Game,"Level1",Game1.BOARD_HEIGHT, Game1.BOARD_WIDHT);
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Instance().PressedKey(Keys.Escape))
            {
                PauseGame = !PauseGame;
            }

            if (!PauseGame)
            {
                level.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            level.Draw(gameTime);

            if (PauseGame)
            {
                //TODO draw overlay
            }
        }
    }
}