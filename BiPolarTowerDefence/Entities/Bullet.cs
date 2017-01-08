using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;
using OpenTK.Graphics.ES30;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace BiPolarTowerDefence.Entities
{
    public class Bullet : BaseObject, IMyGameDrawable, ICollider
    {
        private static List<Bullet> activeBullets = new List<Bullet>();
        private static Queue<Bullet> sleepingBullets = new Queue<Bullet>();
        private BaseObject _owner;
        public TowerType towerType;

        public Texture2D texture;

        public Vector3 velocity = Vector3.Zero;

        public Bullet(Game1 game, Vector3 position, BaseObject owner, TowerType towerType) : base(game, position)
        {
            _owner = owner;
            this.towerType = towerType;
            this.MaxDistanceFromOwner = 100;
            this.texture = new Texture2D(game.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
            this.texture.SetData(new[]{Color.White});


        }

        public override void Update(GameTime gameTime)
        {
            this.position += this.velocity;

            var distanceFromOwner = this.position - _owner.position;
            if (distanceFromOwner.Length() > this.MaxDistanceFromOwner)
            {
                activeBullets.Remove(this);
                sleepingBullets.Enqueue(this);
                this.Enabled = false;
            }

            base.Update(gameTime);
        }

        public float MaxDistanceFromOwner { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture,this.GetRect(),Color.Red);
        }

        public void OnCollision(ICollidable collidable)
        {
            this.Enabled = false;
        }

        public static Bullet SpawnBullet(Level level, Vector3 position, Vector3 velocity, BaseObject owner, float maxRange, TowerType towerType)
        {
            var game = level._game;
            Bullet bullet;
            if (sleepingBullets.Count > 0)
            {
                bullet = sleepingBullets.Dequeue();
                activeBullets.Add(bullet);
                bullet.position = position;
                bullet.velocity = velocity;
                bullet._owner = owner;
                bullet.MaxDistanceFromOwner = maxRange;
                bullet.towerType = towerType;
            }
            else
            {
                bullet = new Bullet(game, position, owner, towerType)
                {
                    velocity = velocity,
                    MaxDistanceFromOwner = maxRange
                };
                level.AddComponent(bullet);

            }

            bullet.Enabled = true;
            return bullet;
        }

        public Rectangle GetHitbox()
        {
            return this.GetRect();
        }
    }
}