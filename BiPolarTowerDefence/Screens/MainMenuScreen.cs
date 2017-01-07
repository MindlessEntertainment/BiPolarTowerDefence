using System.Collections.Generic;
using System.Linq;
using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Screens
{
    public enum MenuButtons
    {
        START,
        EDIT
    }

    public class MainMenuScreen : GameScreen
    {
        List<MenuButton> menuButtons = new List<MenuButton>();
        int selectedButton = 0;

        public MainMenuScreen()
        {

        }

        public override void Load()
        {
            SpriteFont font         = ScreenManager.Game.Font;
            Vector2 size 	        = new Vector2(200,100);
            float buttonX           = ((Game1.GAME_WIDTH / 2)- (size.X/2));
            float buttonY = 200f;

            MenuButton start = new MenuButton(this, new Vector2 (buttonX,200f), size,MenuButtons.START, font);
            MenuButton edit = new MenuButton(this, new Vector2 (buttonX,400f), size, MenuButtons.EDIT, font);
            menuButtons.Add(start);
            menuButtons.Add(edit);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in menuButtons)
            {
                button.Update();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphics = ScreenManager.Game.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.Game.spriteBatch;
            graphics.Clear(Color.DarkSlateGray);
            spriteBatch.Begin();
            spriteBatch.DrawString(Game1.Game.Font,"BiPolar Tower Defence", new Vector2(200f,100f), Color.White,0f,Vector2.Zero,2f,SpriteEffects.None,0f);
            spriteBatch.End();

            foreach (var button in menuButtons)
            {
                button.Draw(gameTime,graphics, spriteBatch);
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
                case MenuButtons.EDIT:

                    break;
            }
        }
    }
}