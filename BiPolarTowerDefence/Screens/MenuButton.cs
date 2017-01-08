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
        private SpriteFont _font;
        private MainMenuScreen _screen;
        private Color _buttonColor;
        public MenuButtons Menubutton { get; protected set; }

        public MenuButton(MainMenuScreen screen, Vector2 position, Vector2 size, MenuButtons menubutton, SpriteFont font)
        {
            _screen = screen;
            _size = size;
            _position = position;
            _font = font;
            Menubutton = menubutton;
            _buttonColor = Color.LightGray;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var pos =  mouseState.Position;
            var rect = this.GetRect();
            _buttonColor = Color.LightGray;

            if (rect.Contains(pos))
            {
                _buttonColor = Color.DarkGray;
            }

            if (rect.Contains(pos) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _screen.HandleClick(Menubutton);
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            int stringX =  (int)(((_size.X/2)-(_font.MeasureString(Menubutton.ToString()).X / 2)) +_position.X);
            int stringY =  (int)(((_size.Y/2)-(_font.MeasureString(Menubutton.ToString()).Y / 2)) +_position.Y);
            Vector2 stringPosition = new Vector2 (stringX,stringY);
            Rectangle destination = new Rectangle (_position.ToPoint(), _size.ToPoint());

            Texture2D pixel = new Texture2D (graphics, 1,1,false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Begin();
            spriteBatch.Draw(pixel, destination, Rectangle.Empty, _buttonColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font,Menubutton.ToString(), stringPosition,Color.Black);
            spriteBatch.End();
        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)this._position.X, (int)this._position.Y, (int)this._size.X,(int)this._size.Y);
        }
    }
}