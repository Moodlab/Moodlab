using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Moodlab
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        //Testing Render
        const int TILE_SIZE = 10;
        readonly Vector screenSize = new Vector(800, 600);
        const int cameraSpeed = 2;
        Texture2D nullTexture;
        Vector cameraPosition = new Vector(0, 0);


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;
            graphics.ApplyChanges();

            map = new Map(new Vector(99, 99));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            map.Generate();

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
            nullTexture = new Texture2D(GraphicsDevice, 1, 1);
            nullTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                cameraPosition.Y -= cameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                cameraPosition.Y += cameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                cameraPosition.X -= cameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                cameraPosition.X += cameraSpeed;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Vector screenTileCount = screenSize.Map(i => i / TILE_SIZE + 1);
            Vector start = cameraPosition.Map(i => Math.Max(0, i / TILE_SIZE));
            Vector offset = cameraPosition.Map(i => i > 0 ? i % TILE_SIZE : -i);

            for (int x = 0; x < Math.Min(screenTileCount.X, map.Size.X-start.X); x++)
            {
                for (int y = 0; y < Math.Min(screenTileCount.Y, map.Size.Y-start.Y); y++)
                {
                    Tile tile = map.Data[x + start.X, y + start.Y];
                    if(tile != null)
                        spriteBatch.Draw(
                            nullTexture,
                            new Rectangle(x * TILE_SIZE - offset.X, y * TILE_SIZE - offset.Y, TILE_SIZE, TILE_SIZE),
                            tile.Color
                        );
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
