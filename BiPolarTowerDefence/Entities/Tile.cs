using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tile: BaseObject, IMyGameDrawable
    {
        private static Texture2D texture;

        public const int TILE_SIZE = 32;

        private readonly Game1 _game;
        public TileType Type;

        private int offset = 0;

        public Tile(Game1 game, Vector3 position, TileType type) : base(game, position)
        {
            _game = game;
            Type = type;
            LoadContent(game);
            offset = _game.random.Next(0, 3)*2;
            this.height = TILE_SIZE;
            this.width = TILE_SIZE;
        }

        private static void LoadContent(Game1 game)
        {
            if (texture == null)
            {
                texture = game.Content.Load<Texture2D>("tileset");
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,this.GetRect(),new Rectangle(offset * TILE_SIZE ,(int)Type * TILE_SIZE, TILE_SIZE, TILE_SIZE),Color.White);
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