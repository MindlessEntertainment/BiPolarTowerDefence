using System;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tower: BaseObject, IMyGameDrawable
    {
        private readonly Level _level;
        private Texture2D _texture;
        private int _count = 0;

        private float projectileSpeed = 25f;
        public TowerType type = TowerType.Normal;

        public Tower(Level level, Vector3 position) : base(level._game, position)
        {
            _level = level;
            this.Initialize();
        }

        public override void Initialize()
        {
            this._texture = Game.Content.Load<Texture2D>("tower");
            base.Initialize();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, new Vector2(this.position.X,this.position.Z),new Rectangle(0,0,100,100),Color.White,0f,new Vector2(50,80),0.5f,SpriteEffects.None, 0f );
        }

        public override void Update(GameTime gameTime)
        {
            if (_count++ % 10 == 0)
            {
                var shotVector = new Vector3(1, -1, 0);
                shotVector.Normalize();
                Bullet.SpawnBullet(_level, this.position + new Vector3(0,0,0), shotVector*projectileSpeed, this, 500);
            }
            base.Update(gameTime);
        }
    }
}