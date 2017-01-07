using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tower: BaseObject, IMyGameDrawable
    {
        private Texture2D texture;

        public Tower(Game game, Vector3 position) : base(game, position)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.texture = Game.Content.Load<Texture2D>("tower");
            base.Initialize();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(this.position.X,-this.position.Y),new Rectangle(0,0,100,100),Color.White,0f,new Vector2(50,50),1f,SpriteEffects.None, 0f );
        }

        private int count = 0;
        public override void Update(GameTime gameTime)
        {
            if (count++ % 10 == 0)
            {
                Bullet.SpawnBullet(Game, this.position + new Vector3(0,-0,0), new Vector3(1, -1, 0)*3, this, 500);
            }
            base.Update(gameTime);
        }
    }
}