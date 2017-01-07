using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public interface IMyGameDrawable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}