using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class TowerButton : BaseButton
    {
        public TowerButton(Game1 game, Vector3 position, string text) : base(game, position, text)
        {
            ChangeSize(new Vector2(40, 20));

        }

        public override void HandleClick()
        {

        }
    }
}