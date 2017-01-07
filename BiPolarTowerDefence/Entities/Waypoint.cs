using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class Waypoint
    {
        public Vector3 position;

        public Waypoint(Vector3 position)
        {
            this.position = position;
        }

        public Waypoint(float X, float Z)
        {
            this.position = new Vector3(X,0,Z);
        }

        public Waypoint(float X, float Y, float Z)
        {
            this.position = new Vector3(X,Y,Z);
        }
    }
}