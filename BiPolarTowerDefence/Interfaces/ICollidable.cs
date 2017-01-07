namespace BiPolarTowerDefence.Entities
{
    public interface ICollidable
    {
        void OnCollision(ICollider collider);
    }
}