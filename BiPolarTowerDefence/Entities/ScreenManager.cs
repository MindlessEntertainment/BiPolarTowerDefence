using System;
using System.Collections.Generic;
using System.Linq;
using BiPolarTowerDefence.Screens;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public enum GameScreens
    {
        SplashScreen,
        MainMenu,
        GamePlay,
        GameOver,
        Credits
    }

    public class ScreenManager: GameComponent
    {
        public Dictionary<GameScreens,BaseScreen> screens = new Dictionary<GameScreens,BaseScreen>();
        public Game1 Game;

        public ScreenManager(Game1 game) : base(game)
        {
            Game = game;
        }

        public void AddScreen(GameScreens key ,BaseScreen screen)
        {
            screen.ScreenManager = this;
            this.screens.Add(key, screen);
        }

        public void RemoveScreen(GameScreens key)
        {
            this.screens.Remove(key);
        }

        public void ActivateScreen(GameScreens key,bool disableAllElse = true)
        {
            if (disableAllElse)
            {
                foreach (var screen in screens)
                {
                    screen.Value.Deactivate();
                }
            }
            this.screens[key].Activate();
        }

        public void DeactivateScreen(GameScreens key)
        {
            this.screens[key].Deactivate();
        }

        public void Load()
        {
            foreach (var entry in screens)
            {
                entry.Value.Load();
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entry in screens)
            {
                var screen = entry.Value;
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var entry in screens)
            {
                var screen = entry.Value;
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
    }
}