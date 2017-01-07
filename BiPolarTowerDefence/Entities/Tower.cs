using System;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tower: BaseObject, IMyGameDrawable
    {
        private Texture2D _texture;
        private int _count = 0;

        public Tower(Game1 game, Vector3 position) : base(game, position)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this._texture = Game.Content.Load<Texture2D>("tower");
            base.Initialize();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, new Vector2(this.position.X,this.position.Z),new Rectangle(0,0,100,100),Color.White,0f,new Vector2(50,50),1f,SpriteEffects.None, 0f );
        }

        public override void Update(GameTime gameTime)
        {
            if (_count++ % 10 == 0)
            {
                Bullet.SpawnBullet(_game, this.position + new Vector3(0,0,0), new Vector3(1, -1, 0)*3, this, 500);
            }
            base.Update(gameTime);
        }
    }
}