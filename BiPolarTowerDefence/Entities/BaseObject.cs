using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class BaseObject :GameComponent
    {
        protected readonly Game1 _game;
        public Vector3 position;
        public float width = 16;
        public float height = 16;

        public BaseObject(Game1 game, Vector3 position):base(game)
        {
            _game = game;
            this.position = position;
        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)this.position.X, (int)this.position.Z, (int)this.width,(int)this.height);
        }

        public Vector2 GetPosition2()
        {
            return new Vector2(position.X,position.Z);
        }
    }
}