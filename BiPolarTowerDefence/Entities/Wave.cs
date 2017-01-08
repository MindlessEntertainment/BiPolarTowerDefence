using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public class Wave
    {
        public Vector3 TheWave;

        public Wave(Vector3 position)
        {
            this.TheWave = position;
        }
        public Wave(int Frosty, int Fiery, int Earthy)
        {
            this.TheWave = new Vector3(Frosty,Fiery,Earthy);
        }
    }
}