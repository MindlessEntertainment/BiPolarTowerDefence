using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Entities
{
    public class Tile: BaseObject, IMyGameDrawable
    {
        private static Texture2D texture;

        public const int TILE_SIZE = 48;
        public const int TILE_SOURCE_SIZE = 300;

        private readonly Game1 _game;
        public TileType Type;

        private int offset = 0;

        public Tile(Game1 game, Vector3 position, TileType type) : base(game, position)
        {
            _game = game;
            Type = type;
            LoadContent(game);
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
            var index = TileTextureIndex.Grass;
            switch (Type)
            {
                case TileType.Path:
                    index = TileTextureIndex.Path;
                    break;
                case TileType.Grass:
                    index = TileTextureIndex.Grass;
                    break;
                case TileType.StonyGrass:
                    index = TileTextureIndex.StonyGrass;
                    break;
                case TileType.ChippyGrass:
                    index = TileTextureIndex.ChippyGrass;
                    break;
                case TileType.DirtyGrass:
                    index = TileTextureIndex.DirtyGrass;
                    break;
            }
            spriteBatch.Draw(texture,this.GetRect(),new Rectangle((int)index * TILE_SOURCE_SIZE ,0 * TILE_SOURCE_SIZE, TILE_SOURCE_SIZE, TILE_SOURCE_SIZE),Color.White);
            //spriteBatch.DrawString(_game._font,offset.ToString(),new Vector2(position.X,position.Z) + new Vector2(8,8),Color.White );
        }
    }

    public enum TileType
    {
        Path,
        Grass,
        StonyGrass,
        ChippyGrass,
        DirtyGrass,
    }

    public enum TileTextureIndex
    {
        Grass,
        StonyGrass,
        ChippyGrass,
        DirtyGrass,
        Path,
    }
}