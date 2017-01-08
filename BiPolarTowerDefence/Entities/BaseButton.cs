using BiPolarTowerDefence.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenTK;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace BiPolarTowerDefence.Entities
{
    public abstract class BaseButton : BaseObject
    {
        private Color buttonColor;
        private Color hoverButtonColor;
        private Color currentbuttonColor;
        private string text;

        public BaseButton(Game1 game, Vector3 position, string text) : base(game, position)
        {
            this.text = text;
            this.buttonColor = Color.LightGray;
            this.hoverButtonColor = Color.DarkGray;
        }

        public void ChangeSize(Vector2 size)
        {
            width = size.X;
            height = size.Y;
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var pos =  mouseState.Position;
            var rect = this.GetRect();
            currentbuttonColor = buttonColor;

            if (rect.Contains(pos))
            {
                currentbuttonColor = hoverButtonColor;
            }

            if (rect.Contains(pos) && mouseState.LeftButton == ButtonState.Pressed)
            {
                this.HandleClick();
            }
        }

        public void Draw(GameTime gameTime)
        {
            var spriteBatch = Game1.Game.spriteBatch;
            var graphics = Game1.Game.GraphicsDevice;
            var font = Game1.Game.Font;
            var pos = GetPosition2();
            int stringX =  (int)(((width/2)-(font.MeasureString(text).X / 2)) +pos.X);
            int stringY =  (int)(((height/2)-(font.MeasureString(text).Y / 2)) +pos.Y);
            Vector2 stringPosition = new Vector2 (stringX,stringY);
            Rectangle destination = GetRect();

            Texture2D pixel = new Texture2D (graphics, 1,1,false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Begin();
            spriteBatch.Draw(pixel, destination, Rectangle.Empty, currentbuttonColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font,text, stringPosition,Color.Black);
            spriteBatch.End();
        }

        public virtual void HandleClick()
        {
        }
    }
}