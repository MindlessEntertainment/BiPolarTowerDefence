using System;
using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Screens
{
    public class LogoScreen : BaseScreen
    {
        private Texture2D logo;
        int mAlphaValue = 1;
        int mFadeIncrement = 3;
        double mFadeDelay = .025;

        public LogoScreen()
        {
        }

        public override void Load()
        {
            logo = Game1.Game.Content.Load<Texture2D>("logo");
        }

        public override void Update(GameTime gameTime)
        {
            mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (mFadeDelay <= 0)
            {
                mAlphaValue += mFadeIncrement;
            }

            if (gameTime.TotalGameTime.Seconds-ActivationTime > 5)
            {
                this.ScreenManager.ActivateScreen(GameScreens.MainMenu);
            }
        }

        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice graphics = Game1.Game.GraphicsDevice;
            SpriteBatch spriteBatch = Game1.Game.spriteBatch;
            graphics.Clear(Color.White);

            var posX = (Game1.GAME_WIDTH - (logo.Width*0.5f)) / 2;
            var posY = (Game1.GAME_HEIGHT - (logo.Height*0.5f)) / 2;
            spriteBatch.Begin();
            spriteBatch.Draw(logo, new Vector2(posX,posY),null,new Color(255,255,255, (byte)MathHelper.Clamp(mAlphaValue,0,255)),0f,Vector2.Zero,0.5f,SpriteEffects.None,0f);
            spriteBatch.End();
        }
    }
}