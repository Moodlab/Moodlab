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
        readonly Position2 screenSize = new Position2(1280, 720);
        Texture2D nullTexture;
        int tileSize = Tiles.Tile.SIZE;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;
        Entities.Player player;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;
            graphics.ApplyChanges();

            map = new Map(new Position2(11, 11));
            player = new Entities.Player();
            map.AddEntity(player);
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

            map.Update(gameTime);

            //Zoom
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                tileSize++;

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && tileSize > 1)
                tileSize--;

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

            Position2 screenTileCount = screenSize.Map(i => i / tileSize);

            for (int x = Math.Max(0, (int)Math.Floor(player.Position.X - screenTileCount.X / 2)); x < Math.Min(map.Size.X, (int)Math.Ceiling(player.Position.X + screenTileCount.X / 2) + 1); x++)
            {
                for (int y = Math.Max(0, (int)Math.Floor(player.Position.Y - screenTileCount.Y / 2)); y < Math.Min(map.Size.Y, (int)Math.Ceiling(player.Position.Y + screenTileCount.Y / 2) + 1); y++)
                {
                    Tiles.Tile tile = map.TileMap[x, y];
                    if(tile != null)
                        spriteBatch.Draw(
                            nullTexture,
                            new Rectangle((int)((x - player.Position.X - 0.5f) * tileSize) + screenSize.X / 2, (int)((y - player.Position.Y - 0.5f) * tileSize) + screenSize.Y / 2, tileSize, tileSize),
                            tile.Color
                        );
                }
            }

            spriteBatch.Draw(nullTexture, new Rectangle((screenSize.X - (int)(player.Size.X * tileSize)) / 2, (screenSize.Y - (int)(player.Size.X * tileSize)) / 2, (int)(player.Size.X * tileSize), (int)(player.Size.Y * tileSize)), Color.GreenYellow);
            //Console.WriteLine(player.Position);

            //DEBUG
            spriteBatch.Draw(nullTexture, new Rectangle(screenSize.X / 2, screenSize.Y / 2, 2, 2), Color.LimeGreen);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
