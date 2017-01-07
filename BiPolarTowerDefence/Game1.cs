using System;
using BiPolarTowerDefence.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
	    public const int BOARD_WIDHT = 20;
	    public const int BOARD_HEIGHT = 10;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		GameState _state;
		SpriteFont _font;

	    private Level level;


	    public MouseState mouseState;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		    this.IsMouseVisible = true;
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

		    this.level = new Level(this,"Level1",BOARD_HEIGHT, BOARD_WIDHT);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
		    this.mouseState = Mouse.GetState();
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
	        this.level.Update(gameTime);
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
	        this.level.Draw(gameTime);
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
			Point position 	= new Point (0,0);
			Point size 		= new Point (100,50);
			Vector2 stringPosition = new Vector2 (25,15);
			Rectangle destination = new Rectangle (position, size);

			Texture2D pixel = new Texture2D (this.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });

			spriteBatch.Begin();
			spriteBatch.Draw(pixel, destination, Rectangle.Empty, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
			spriteBatch.DrawString(_font,"START", stringPosition,Color.Black);
			spriteBatch.End();
	    }
	}

	enum GameState
	{
		MainMenu,
		Gameplay,
		EndCredits
	}
}

