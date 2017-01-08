using Microsoft.Xna.Framework.Graphics;

namespace BiPolarTowerDefence.Utilities
{
    public class Fonts
    {
        public static SpriteFont Ariel14Bold { get; } = Game1.Game.Content.Load<SpriteFont>("font");
        public static SpriteFont Ariel40 { get; } = Game1.Game.Content.Load<SpriteFont>("Fonts/Ariel40");
        public static SpriteFont Ariel44 { get; } = Game1.Game.Content.Load<SpriteFont>("Fonts/Ariel44");
        public static SpriteFont Ariel48 { get; } = Game1.Game.Content.Load<SpriteFont>("Fonts/Ariel48");
        public static SpriteFont Ariel54 { get; } = Game1.Game.Content.Load<SpriteFont>("Fonts/Ariel54");
    }
}