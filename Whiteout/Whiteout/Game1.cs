using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Whiteout
{
    public enum GameState
    {
        Title = 1,
        MainMenu = 2,
        GamePlay = 3,
        Upgrade = 4
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameState _gameState;
        Level _currentLevel;
        Player _player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _gameState = new GameState();
            _gameState = GameState.GamePlay;
            _player = new Player(Content);
            _currentLevel = new Level(Content, _player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch (_gameState)
            {
                case GameState.Title:

                    break;
                case GameState.MainMenu:

                    break;

                case GameState.GamePlay:
                    _currentLevel.Update(gameTime);
                    break;

                case GameState.Upgrade:

                    break;

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GhostWhite);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (_gameState)
            {
                case GameState.Title:

                    break;
                case GameState.MainMenu:
                    
                    break;

                case GameState.GamePlay:
                    _currentLevel.Draw(spriteBatch, gameTime);
                    break;

                case GameState.Upgrade:

                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
