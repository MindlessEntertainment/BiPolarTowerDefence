using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Enemy: BaseObject, IMyGameDrawable, ICollidable
    {
        public Queue<Enemy> enemyReuseQueue = new Queue<Enemy>();
        private readonly Game1 _game;
        private Texture2D texture;

        public Direction myDirection;
        private Texture2D textureHealth;

        public int Life { get; private set; }
        public int MaxLife = 3;
        public int LifeWidth = 50;
        public const int spriteWidth = 50;
        public const int spriteHeight = 50;

        public Enemy(Game1 game, Vector3 position) : base(game, position)
        {
            _game = game;
            this.Initialize();
        }

        public override void Initialize()
        {
            this.texture = Game.Content.Load<Texture2D>("enemy");
            this.Life = 3;

            this.textureHealth = new Texture2D(_game.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
            this.textureHealth.SetData(new[]{Color.White});
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.position += Vector3.Right + Vector3.Backward;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X,this.position.Z),new Rectangle(0,0,spriteWidth,spriteHeight),Color.White);

            spriteBatch.Draw(this.textureHealth,new Rectangle((int)position.X-1,(int)position.Z-1, LifeWidth, 7),Color.Gray);
            spriteBatch.Draw(this.textureHealth,new Rectangle((int)position.X,(int)position.Z, LifeWidth/MaxLife*Life, 5),Color.Green);
        }

        public void OnCollision(ICollider collider)
        {
            var bullet = collider as Bullet;
            if (bullet != null)
            {
                this.Life--;
                if (Life < 1)
                {
                    this.Enabled = false;
                    this.enemyReuseQueue.Enqueue(this);
                    
                }
                Console.WriteLine("HIT! Life:" + this.Life);
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