using OpenTK;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace BiPolarTowerDefence.Entities
{
    public class TowerButton : BaseButton
    {
        public TowerButton(Game1 game, Vector3 position, string text) : base(game, position, text)
        {
            ChangeSize(new Vector2(40, 20));
        }

        public void PositionUpdate(Vector3 pos)
        {
            position = pos;
        }

        public override void HandleClick()
        {
            if (this.text == "+")
            {
                this.TowerButtonMenu.Tower.TierUp();
            }
            else
            {

            }
        }

        public TowerButtonMenu TowerButtonMenu { get; set; }
    }
}