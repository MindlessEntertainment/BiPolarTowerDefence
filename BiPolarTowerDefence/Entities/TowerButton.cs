using OpenTK;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace BiPolarTowerDefence.Entities
{
    public class TowerButton : BaseButton
    {
        private readonly TowerButtonMenu _menu;

        public TowerButton(Game1 game, Vector3 position, string text,TowerButtonMenu menu) : base(game, position, text)
        {
            _menu = menu;
            ChangeSize(new Vector2(40, 30));
        }

        public void PositionUpdate(Vector3 pos)
        {
            position = pos;
        }

        public override void HandleClick()
        {
            if (this.text == "+")
            {
                this._menu.Tower.TierUp();
            }
            else if(this.text == "1")
            {
                this._menu.Tower.type = TowerType.Earthy;
            }
            else if(this.text == "2")
            {
                this._menu.Tower.type = TowerType.Fiery;
            }
            else if(this.text == "3")
            {
                this._menu.Tower.type = TowerType.Frosty;
            }
        }
    }
}