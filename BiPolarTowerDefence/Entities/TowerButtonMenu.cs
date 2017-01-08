using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class TowerButtonMenu : BaseObject
    {
        public List<TowerButton> TowerButtons { get; set; } = new List<TowerButton>();
        public Tower Tower { get; set; }
        public bool Active { get; set; } = false;

        public TowerButtonMenu(Game1 game, Vector3 position) : base(game, position)
        {
            var buttonWidth = 40;
            Vector3 offset = new Vector3(buttonWidth, 0, 0);
            width = 120;
            height = 20;

            TowerButtons.Add(new TowerButton(game , position+offset*1, "+",this));
            TowerButtons.Add(new TowerButton(game , position+offset*2, "2",this));
            TowerButtons.Add(new TowerButton(game , position+offset*3, "3",this));
        }

        public void Update(GameTime gameTime)
        {
            foreach (TowerButton button in this.TowerButtons)
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

        public void PositionUpdate(Vector3 pos)
        {
            position = pos;
            var buttonWidth = 40;
            Vector3 offset = new Vector3(buttonWidth, 0, 0);

            for (int i = 1; i < TowerButtons.Count+1; i++)
            {
                var ble = i - 1;
                var button = TowerButtons[ble];
                button.PositionUpdate(position+(offset*ble));
            }
        }
    }
}