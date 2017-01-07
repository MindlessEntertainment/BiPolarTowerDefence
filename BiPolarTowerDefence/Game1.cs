using System;
using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
	    public const int BOARD_WIDHT = 1024/Tile.TILE_SIZE;
	    public const int BOARD_HEIGHT = 768/Tile.TILE_SIZE;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		GameState _state;
		public SpriteFont _font;

	    public Random random = new Random();

	    private Level level;


	    public MouseState mouseState;

	    public ScreenManager ScreenManager { get; set; }

	    public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		    IsMouseVisible = true;

		    Console.WriteLine("Supported display modes");
		    var displayModes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;

		    foreach (var item in displayModes)
		    {
		        Console.WriteLine("Aspect Ratio: "+item.AspectRatio+", Height: "+item.Height+", Width: "+item.Width+"}");
		    }

		    graphics.PreferredBackBufferWidth = BOARD_WIDHT * Tile.TILE_SIZE;
		    graphics.PreferredBackBufferHeight = BOARD_HEIGHT * Tile.TILE_SIZE;
		    graphics.ApplyChanges();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
		    _state = GameState.Gameplay;
			_font = this.Content.Load<SpriteFont> ("font");

		    base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

		    //TODO: use this.Content to load your game content here

			this.ScreenManager = new ScreenManager(this);
		    level = new Level(this,"Level1",BOARD_HEIGHT, BOARD_WIDHT);
		}


	    /// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
		    mouseState = Mouse.GetState();
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif

			// TODO: Add your update logic here
            switch (_state)
			{
			case GameState.MainMenu:
				UpdateMainMenu (gameTime);
				break;
			case GameState.Gameplay:
				UpdateGameplay (gameTime);
				break;
			case GameState.EndCredits:
				UpdateEndCredits (gameTime);
				break;
			default:
				break;
			}

			base.Update (gameTime);
		}

		private void UpdateEndCredits(GameTime gameTime)
	    {
	        throw new NotImplementedException();
	    }

	    private void UpdateGameplay(GameTime gameTime)
	    {
	        level.Update(gameTime);
	    }

	    private void UpdateMainMenu(GameTime gameTime)
	    {
	    }

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

		    switch (_state)
		    {
		        case GameState.MainMenu:
		            DrawMainMenu (gameTime);
		            break;
		        case GameState.Gameplay:
		            DrawGameplay (gameTime);
		            break;
		        case GameState.EndCredits:
		            DrawEndCredits (gameTime);
		            break;
		        default:
		            break;
		    }

			base.Draw (gameTime);
		}

		private void DrawEndCredits(GameTime gameTime)
	    {
	        throw new NotImplementedException();
	    }

	    private void DrawGameplay(GameTime gameTime)
	    {
	        level.Draw(gameTime);
	        /*spriteBatch.Begin();
		    foreach (var item in this.Components)
		    {
		        var drawable = item as IMyGameDrawable;
		        if (drawable != null)
		        {
		            drawable.Draw(gameTime, spriteBatch);
		        }
		    }
		    spriteBatch.End();
            */
	    }

		private void DrawMainMenu(GameTime gameTime)
	    {

	    	graphics.GraphicsDevice.Clear (Color.DarkSlateGray);

	        Vector2 size 	        = new Vector2(200,100);
	        float buttonX = ((this.Window.ClientBounds.Width / 2)- (size.X/2));
	        float buttonY = 200f;

	        MenuButton start = new MenuButton(new Vector2 (buttonX,200f), size,"START", _font);
	        MenuButton edit = new MenuButton(new Vector2 (buttonX,300f), size, "EditMode", _font);
	        start.Draw(gameTime,this.GraphicsDevice, spriteBatch);
	        edit.Draw(gameTime,this.GraphicsDevice, spriteBatch);
	    }
	}

	enum GameState
	{
		MainMenu,
		Gameplay,
		EndCredits
	}

    class MenuButton
    {
        private Vector2 _position;
        private Vector2 _size;
        private string _text;
        private SpriteFont _font;

        public MenuButton(Vector2 position, Vector2 size, string text, SpriteFont font)
        {
            _size = size;
            _position = position;
            _text = text;
            _font = font;

        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            int stringX =  (int)(((_size.X/2)-(_font.MeasureString(_text).X / 2)) +_position.X);
            int stringY =  (int)(((_size.Y/2)-(_font.MeasureString(_text).Y / 2)) +_position.Y);
            Vector2 stringPosition = new Vector2 (stringX,stringY);
            Rectangle destination = new Rectangle (_position.ToPoint(), _size.ToPoint());

            Texture2D pixel = new Texture2D (graphics, 1,1,false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            spriteBatch.Begin();
            spriteBatch.Draw(pixel, destination, Rectangle.Empty, Color.LightGray, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font,_text, stringPosition,Color.Black);
            spriteBatch.End();
        }
    }
}

