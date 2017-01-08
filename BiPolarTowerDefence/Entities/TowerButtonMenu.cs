using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class TowerButtonMenu : BaseObject
    {
        public List<TowerButton> TowerButtons { get; set; } = new List<TowerButton>();
        public Tower Tower { get; set; }

        public TowerButtonMenu(Game1 game, Vector3 position) : base(game, position)
        {
            var buttonWidth = 40;
            Vector3 offset = new Vector3(buttonWidth, 0, 0);
            width = 120;
            height = 20;


            TowerButtons.Add(new TowerButton(game , position+offset*1, "1"));
            TowerButtons.Add(new TowerButton(game , position+offset*2, "2"));
            TowerButtons.Add(new TowerButton(game , position+offset*3, "3"));
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (var button in TowerButtons)
            {

                button.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var button in TowerButtons)
            {
                button.Draw(gameTime);
            }
        }
    }
}