using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Interfaces
{
    public interface ICollider
    {
        Rectangle GetHitbox();
        void OnCollision(ICollidable collidable);
    }
}