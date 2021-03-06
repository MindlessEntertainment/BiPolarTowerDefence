﻿using BiPolarTowerDefence.Entities;
using BiPolarTowerDefence.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Screens
{
    public class GameOverScreen : BaseScreen
    {
        public override void Load()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (ActivationTime == 0)
            {
                ActivationTime = gameTime.TotalGameTime.Seconds;
            }

            if (gameTime.TotalGameTime.Seconds-ActivationTime > 2)
            {
                ActivationTime = 0;
                this.ScreenManager.ReloadScreen(GameScreens.GamePlay);
                this.ScreenManager.ActivateScreen(GameScreens.Credits);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphics = Game1.Game.GraphicsDevice;
            SpriteBatch spriteBatch = Game1.Game.spriteBatch;
            graphics.Clear(Color.Black);
            var text = "Game Over";
            var size = Fonts.Ariel54.MeasureString(text);
            var posX = (Game1.GAME_WIDTH - size.X) / 2;
            var posY = (Game1.GAME_HEIGHT - size.X) / 2;
            spriteBatch.Begin();
            spriteBatch.DrawString (Fonts.Ariel54, text, new Vector2 (posX, posY), Color.White);
            spriteBatch.End();
        }
    }
}