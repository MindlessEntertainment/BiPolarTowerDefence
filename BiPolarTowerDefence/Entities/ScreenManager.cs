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
        GamePlay
    }

    public class ScreenManager: GameComponent
    {
        public Dictionary<GameScreens,GameScreen> screens = new Dictionary<GameScreens,GameScreen>();
        public Game1 Game;

        public ScreenManager(Game1 game) : base(game)
        {
            Game = game;
        }

        public void AddScreen(GameScreens key ,GameScreen gameScreen)
        {
            gameScreen.ScreenManager = this;
            this.screens.Add(key, gameScreen);
        }

        public void RemoveScreen(GameScreens key)
        {
            this.screens.Remove(key);
        }

        public void Load()
        {
            foreach (var entry in screens)
            {
                entry.Value.Load();
            }
        }

        public void Update(GameTime gameTime)
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