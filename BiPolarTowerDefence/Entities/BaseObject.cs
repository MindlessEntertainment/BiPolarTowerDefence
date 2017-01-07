using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace BiPolarTowerDefence.Entities
{
    public class BaseObject :GameComponent
    {
        public Vector3 position;

        public BaseObject(Game game, Vector3 position):base(game)
        {
            this.position = position;
        }
    }
}