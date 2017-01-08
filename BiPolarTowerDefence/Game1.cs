using System;
using BiPolarTowerDefence.Entities;
using BiPolarTowerDefence.Screens;
using BiPolarTowerDefence.Utilities;
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
	    public const int GAME_WIDTH = 1024;
	    public const int GAME_HEIGHT = 768;

	    public const int BOARD_WIDHT = GAME_WIDTH/Tile.TILE_SIZE;
	    public const int BOARD_HEIGHT = GAME_HEIGHT/Tile.TILE_SIZE;

		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;
		public SpriteFont Font;

	    public Random random = new Random();

	    public static Game1 Game;

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

		    Game = this;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{

			Font = Fonts.Ariel14Bold;
		    ScreenManager = new ScreenManager(this);
		    var splashScreen = new LogoScreen();
		    splashScreen.Activate();
		    var menu = new MainMenuScreen();
		    ScreenManager.AddScreen(GameScreens.SplashScreen, splashScreen);
		    ScreenManager.AddScreen(GameScreens.MainMenu, menu);
		    ScreenManager.AddScreen(GameScreens.GamePlay, new GameplayScreen());
		    ScreenManager.AddScreen(GameScreens.GameOver, new GameOverScreen());
		    ScreenManager.AddScreen(GameScreens.Credits, new CreditsScreen());
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
		    this.ScreenManager.Load();
		}


	    /// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
		    Input.Instance().Update();
		    mouseState = Mouse.GetState();
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif

			// TODO: Add your update logic here
            this.ScreenManager.Update(gameTime);

		    base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
		    this.ScreenManager.Draw(gameTime);

			base.Draw (gameTime);
		}
	}
}

