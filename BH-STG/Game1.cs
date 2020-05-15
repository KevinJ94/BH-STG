using System.Collections.Generic;
using BH_STG.Manager;
using BH_STG.Models;
using BH_STG.Sprites;
using BH_STG.Sprites.Boss;
using BH_STG.States;
using BH_STG.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BH_STG
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static int ScreenWidth = 512;
        public static int ScreenHeight = 768;
        public static ContentManager GlobalContent;
        private State _currentState;
        private State _nextState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GlobalContent = Content;
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, Content);
            _currentState.LoadContent();
            _nextState = null;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
                
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            //     Keyboard.GetState().IsKeyDown(Keys.Escape))
            //     Exit();

            //TODO: Add your update logic here


            base.Update(gameTime);
        }



        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            // spriteBatch.Begin();
            // spriteBatch.Draw(_bg, new Vector2(0, 0), Color.White);
            // foreach (var sprite in _sprites)
            //     sprite.Draw(spriteBatch);
            // spriteBatch.End();
            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}