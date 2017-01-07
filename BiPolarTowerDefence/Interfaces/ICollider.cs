using BiPolarTowerDefence.Entities;

namespace BiPolarTowerDefence.Interfaces
{
    public interface ICollider
    {
        void OnCollision(ICollidable collidable);
    }
}