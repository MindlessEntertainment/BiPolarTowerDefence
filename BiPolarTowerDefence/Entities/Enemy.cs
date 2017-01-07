using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Enemy: BaseObject, IMyGameDrawable
    {
        private Texture2D texture;

        public Enemy(Game game, Vector3 position) : base(game, position)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.texture = Game.Content.Load<Texture2D>("enemy");
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
    }
}