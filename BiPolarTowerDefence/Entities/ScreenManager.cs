using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class ScreenManager: GameComponent
    {
        List<GameScreen> screens = new List<GameScreen>();

        public ScreenManager(Game1 game) : base(game)
        {
        }
    }
}