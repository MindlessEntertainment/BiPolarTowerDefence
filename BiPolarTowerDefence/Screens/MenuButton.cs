using System;
using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Screens
{
    public class MenuButton : BaseButton
    {
        private MainMenuScreen _screen;
        public MenuButtons Menubutton { get; protected set; }

        public MenuButton(Game1 game, Vector3 position,string text, MainMenuScreen screen, MenuButtons menubutton) : base(game,position,text)
        {
            _screen = screen;
            Menubutton = menubutton;
            ChangeSize(new Vector2(200,80));
        }

        public override void HandleClick()
        {
            switch (this.Menubutton)
            {
                case MenuButtons.START:
                    this._screen.ScreenManager.ActivateScreen(GameScreens.GamePlay);
                    break;
                case MenuButtons.EDIT:
                    //this._screen.ScreenManager.ActivateScreen(GameScreens.GamePlay);
                    break;
                case MenuButtons.EXIT:
                    this._game.Exit();
                    break;
            }
        }
    }
}