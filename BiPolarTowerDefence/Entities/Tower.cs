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
            spriteBatch.Draw(this.texture, new Vector2(this.position.Z,this.position.Y),new Rectangle(0,0,100,100),Color.White);
        }
    }
}