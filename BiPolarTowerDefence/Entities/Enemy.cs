using System.Security.Cryptography.X509Certificates;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Enemy: BaseObject, IMyGameDrawable, ICollidable
    {
        private Texture2D texture;

        public Direction myDirection;

        public int Life { get; private set; }

        public Enemy(Game1 game, Vector3 position) : base(game, position)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.texture = Game.Content.Load<Texture2D>("enemy");
            this.Life = 3;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.position += Vector3.Right + Vector3.Down;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X,-this.position.Y),new Rectangle(0,0,50,50),Color.White);
        }

        public void OnCollision(ICollider collider)
        {
            var bullet = collider as Bullet;
            if (bullet != null)
            {
                this.Life--;
            }
        }

        public Rectangle GetHitbox()
        {
            return this.GetRect();
        }

        public bool Intersects(ICollider collider)
        {
            var thisBox = this.GetHitbox();
            var thatBox = collider.GetHitbox();
            return thisBox.Intersects(thatBox);
        }
    }
}