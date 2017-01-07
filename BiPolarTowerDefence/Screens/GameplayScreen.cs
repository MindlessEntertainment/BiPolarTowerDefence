using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Screens
{
    public class GameplayScreen : GameScreen
    {
        private Level level;

        public GameplayScreen()
        {

        }

        public override void Load()
        {
            level = new Level(Game1.Game,"Level1",Game1.BOARD_HEIGHT, Game1.BOARD_WIDHT);
        }

        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            level.Draw(gameTime);
        }
    }
}