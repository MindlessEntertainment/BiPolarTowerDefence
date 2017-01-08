using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using BiPolarTowerDefence.Entities;
using BiPolarTowerDefence.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Screens
{
    public enum MenuButtons
    {
        START,
        EXIT
    }

    public class MainMenuScreen : BaseScreen
    {
        private Texture2D background;
        List<MenuButton> menuButtons = new List<MenuButton>();
		SpriteFont TitleFont;

        public override void Load()
        {
            this.background = Game1.Game.Content.Load<Texture2D>("main-menu");
			TitleFont = Fonts.Ariel54;
            SpriteFont font         = ScreenManager.Game.Font;
            Vector2 size 	        = new Vector2(200,80);
            //float buttonX           = ((Game1.GAME_WIDTH / 2)- (size.X/2));
            float buttonX           = Game1.GAME_WIDTH - size.X - 50;

            MenuButton start = new MenuButton(Game1.Game, new Vector3 (buttonX, 0f,500f),"START",this,MenuButtons.START);
            MenuButton edit = new MenuButton(Game1.Game, new Vector3 (buttonX, 0f,600f), "EXIT",this,MenuButtons.EXIT);
            menuButtons.Add(start);
            menuButtons.Add(edit);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in menuButtons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphics = ScreenManager.Game.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.Game.spriteBatch;
            graphics.Clear(Color.DarkSlateGray);
            spriteBatch.Begin();
            spriteBatch.Draw(this.background,Vector2.Zero,Color.White);
			spriteBatch.DrawString (TitleFont, "BiPolar Tower Defence", new Vector2 (150f, 30f), Color.Black);
            spriteBatch.End();

            foreach (var button in menuButtons)
            {
                button.Draw(gameTime);
            }
        }

        public void HandleClick(MenuButtons button)
        {
            switch (button)
            {
                case MenuButtons.START:
                    Deactivate();
                    var a = this.ScreenManager.screens[GameScreens.GamePlay];
                    a.Activate();
                    break;
                case MenuButtons.EXIT:
                    Game1.Game.Exit();
                    break;
            }
        }
    }
}