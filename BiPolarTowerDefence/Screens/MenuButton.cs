using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Screens
{
    public class MenuButton
    {
        private Vector2 _position;
        private Vector2 _size;
        private MenuButtons _menubutton;
        private SpriteFont _font;
        private MainMenuScreen _screen;

        public MenuButton(MainMenuScreen screen, Vector2 position, Vector2 size, MenuButtons menubutton, SpriteFont font)
        {
            _screen = screen;
            _size = size;
            _position = position;
            _menubutton = menubutton;
            _font = font;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var pos =  mouseState.Position;
            var rect = this.GetRect();

            if (rect.Contains(pos) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _screen.HandleClick(_menubutton);
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            int stringX =  (int)(((_size.X/2)-(_font.MeasureString(_menubutton.ToString()).X / 2)) +_position.X);
            int stringY =  (int)(((_size.Y/2)-(_font.MeasureString(_menubutton.ToString()).Y / 2)) +_position.Y);
            Vector2 stringPosition = new Vector2 (stringX,stringY);
            Rectangle destination = new Rectangle (_position.ToPoint(), _size.ToPoint());

            Texture2D pixel = new Texture2D (graphics, 1,1,false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Begin();
            spriteBatch.Draw(pixel, destination, Rectangle.Empty, Color.LightGray, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font,_menubutton.ToString(), stringPosition,Color.Black);
            spriteBatch.End();
        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)this._position.X, (int)this._position.Y, (int)this._size.X,(int)this._size.Y);
        }
    }
}