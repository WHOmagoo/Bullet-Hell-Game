using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BulletHell.Annotations;
using BulletHell.controls;
using BulletHell.director;
using BulletHell.GameEngine;
using BulletHell.levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BulletHell.Graphics;

namespace BulletHell
{
    public class BHGame : Game
    {
        private static GameDirector director;
        private static Canvas canvas;
        private static CollisionManager collisionManager;

        public static GameDirector Director {get{return director;}}
        public static Canvas Canvas {get{return canvas;}}

        public static CollisionManager CollisionManager { get => collisionManager; }
        private IGameFactory factory;
        private Controller controller;

        public static bool gameOver;
        public static bool won;
        private bool paused;

        public BHGame(IGameFactory factory, Controller controller)
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.factory = factory;
            this.controller = controller;
            controller.OnPause += OnPause;
            controller.OnUnpause += OnUnpause;
        }

        protected void SetGame(IGameFactory factory)
        {
            Tuple<GameDirector, Canvas, CollisionManager> result = factory.makeGame(GraphicsDevice, controller);
            director = result.Item1;
            canvas = result.Item2;
            canvas.PlayerDeathHandler += OnPlayerDeath;
            collisionManager = result.Item3;
//            controller.OnPause += Clock.getClock().OnPause;
        }

        protected override void Initialize()
        {
            GraphicsLoader.makeGraphicsLoader(GraphicsDevice);
            GraphicsLoader.getGraphicsLoader().setGraphicsTexture(new FileStream("Content/sprites/bullet.png", FileMode.Open));

            SetGame(factory);
            DrawingTool.Initialize(GraphicsDevice);
            SpriteFont font = Content.Load<SpriteFont>("SplashFont");
            Canvas.SetFont(font);

            base.Initialize();
        }
       
        protected override void LoadContent()
        {

            // shuttle = Content.Load<Texture2D>("shuttle");
            //scroll1 = new Scroll(Content.Load<Texture2D>("Content/sprites/star.png"), new Rectangle(0,0,800,480));
            //scroll2 = new Scroll(Content.Load<Texture2D>("Content/sprites/star.png"), new Rectangle(0, 800, 800, 480));
        }

        private int updates = 0;

        protected override void Update(GameTime gameTime)
        {
           controller.Update(); 
            
            bool enemy2Flag = false;
            if (!gameOver && !paused) {
                //            Clock.getClock().SetGameTime(gameTime);
                Clock.getClock().UpdateTime(gameTime);
                director.Update();
                collisionManager.runCollisions();
                canvas.Update();

                base.Update(gameTime);
            }
            else if(paused)
            {
                //Do nothing
            }
            else
            {
                KeyboardState currState = Keyboard.GetState();
                if (currState.IsKeyDown(Keys.Enter))
                {
                    Console.WriteLine("start over");
                    SetGame(factory);
                    gameOver = false;
                    won = false;
                    canvas.TurnOffMessage();
                }
                else if (currState.IsKeyDown(Keys.Escape))
                {
                    System.Environment.Exit(0);
                }
            }
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            //TODO make an onscreen prompt for this
            Console.WriteLine("Game Over!");
            //SetGame(factory);
            gameOver = true;
            won = false;
            canvas.SetMessage("Game Over! Press Enter to play again or ESC to leave");
        }

        public static void OnWinCondition()
        {
            Console.WriteLine("Winner!");
            gameOver = true;
            won = true;
            canvas.SetMessage("Nice Win! Press Enter to play again or ESC to leave");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // spriteBatch.Begin();
            canvas.Draw();
            // spriteBatch.End();
            base.Draw(gameTime);

        }

        private void OnPause(object sender, EventArgs e)
        {
            paused = true;
            Console.WriteLine("Paused");
        }

        private void OnUnpause(object sender, EventArgs e)
        {
            paused = false;
            Console.WriteLine("Unpaused");

        }
    }
}