namespace BiPolarTowerDefence.Entities
{
    public interface ICollider
    {
        void OnCollision(ICollidable collidable);
    }
}