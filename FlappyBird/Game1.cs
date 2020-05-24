using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FlappyBird
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D gameOver;
        Bird bird;

        List<Columns> columns = new List<Columns>();

        float delay = 0;

        bool beforeJumpStatus = false;

        bool isGameOver = false;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 288;
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

            base.Initialize();
            background = Content.Load<Texture2D>("background-day");
            gameOver = Content.Load<Texture2D>("gameover");
            bird = new Bird(50,100,new Texture2D[] {Content.Load<Texture2D>("yellowbird-downflap"), Content.Load<Texture2D>("yellowbird-midflap"), Content.Load<Texture2D>("yellowbird-upflap") });


            columns.Add(new Columns(Content.Load<Texture2D>("pipe-green"), 288));
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
            delay += gameTime.ElapsedGameTime.Milliseconds;
            if (delay > 20)
            {
                delay = gameTime.ElapsedGameTime.Milliseconds;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                if (columns.Count == 0)
                {
                    columns.Add(new Columns(Content.Load<Texture2D>("pipe-green"), 320));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !beforeJumpStatus && !bird.isDead)
                {
                    bird.Jump();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    beforeJumpStatus = true;
                }
                else
                {
                    beforeJumpStatus = false;
                }
                if (!bird.isDead)
                {
                    bird.Move(1);
                }
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i].x + columns[i].columnTexture.Width < 0)
                    {
                        columns.RemoveAt(i);
                    }
                }

                for (int i = 0; i < columns.Count; i++)
                {
                    if (!bird.isDead)
                    {
                        columns[i].Move(1);
                        columns[i].Colide(ref bird);
                    }
                }

                if (bird.isDead)
                {
                    isGameOver = true;
                }

                Console.WriteLine(bird.score);
                delay -= 30;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            spriteBatch.Draw(background, new Vector2(0,0), Color.White);

            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].Draw(spriteBatch);
            }

            bird.Draw(spriteBatch);

            if (isGameOver)
            {
                spriteBatch.Draw(gameOver, new Vector2((graphics.PreferredBackBufferWidth / 2) - gameOver.Width / 2, (graphics.PreferredBackBufferHeight / 2) - gameOver.Height / 2), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
