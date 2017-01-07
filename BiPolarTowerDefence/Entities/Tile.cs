using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tile: BaseObject, IMyGameDrawable
    {
        private static Texture2D texture;

        public const int TILE_SIZE = 64;

        private readonly TileType _type;

        public Tile(Game game, Vector3 position, TileType type) : base(game, position)
        {
            _type = type;
            LoadContent(game);
        }

        private static void LoadContent(Game game)
        {
            if (texture == null)
            {
                texture = game.Content.Load<Texture2D>("tileset");
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,new Vector2(position.X,position.Z),new Rectangle(1 * TILE_SIZE ,(int)_type * TILE_SIZE, TILE_SIZE, TILE_SIZE),Color.White);
        }
    }

    public enum TileType
    {
        BasaltStone,
        Desert,
        Grass,
        Dirt,
        SandStone

    }
}