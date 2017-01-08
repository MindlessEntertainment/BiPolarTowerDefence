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
        EDIT,
        EXIT
    }

    public class MainMenuScreen : BaseScreen
    {
        List<MenuButton> menuButtons = new List<MenuButton>();
		SpriteFont TitleFont;

        public override void Load()
        {
			TitleFont = Fonts.Ariel54;
            SpriteFont font         = ScreenManager.Game.Font;
            Vector2 size 	        = new Vector2(200,80);
            float buttonX           = ((Game1.GAME_WIDTH / 2)- (size.X/2));

            MenuButton start = new MenuButton(Game1.Game, new Vector3 (buttonX, 0f,200f),"START",this,MenuButtons.START);
            MenuButton edit = new MenuButton(Game1.Game, new Vector3 (buttonX, 0f,300f), "EDIT",this,MenuButtons.EDIT);
            menuButtons.Add(start);
            menuButtons.Add(edit);
            menuButtons.Add(new MenuButton(Game1.Game, new Vector3 (buttonX, 0f,300f), "EXIT",this,MenuButtons.EXIT));
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
			spriteBatch.DrawString (TitleFont, "BiPolar Tower Defence", new Vector2 (150f, 100f), Color.White);
            spriteBatch.End();

            foreach (var button in menuButtons)
            {
                button.Draw(gameTime);
            }
        }
    }
}