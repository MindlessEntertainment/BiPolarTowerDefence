using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Interfaces
{
    public interface ICollidable
    {
        Rectangle GetHitbox();
        void OnCollision(ICollider collider);
        bool Intersects(ICollider collider);
    }
}