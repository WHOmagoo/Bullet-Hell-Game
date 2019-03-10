using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
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
        public GameDirector director {get;}
        public Canvas canvas {get;}
        public CollisionManager collisionManager {get;}

        private IGameFactory factory;


        public BHGame(Canvas canvas, GameDirector gameDirector, CollisionManager collisionManager)
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.director = gameDirector;
            this.canvas = canvas;
            this.collisionManager = collisionManager;

        }

        protected override void Initialize()
        {
            GraphicsLoader.makeGraphicsLoader(GraphicsDevice);
            GraphicsLoader.getGraphicsLoader().setGraphicsTexture(new FileStream("Content/sprites/bullet.png", FileMode.Open));
            DrawingTool.Initialize(GraphicsDevice);

            // Tuple<GameDirector, Canvas, CollisionManager> result = factory.makeGame(GraphicsDevice);
            // director = result.Item1;
            // canvas = result.Item2;
            // collisionManager = result.Item3;
            
            base.Initialize();
        }
       
        protected override void LoadContent()
        {
            // shuttle = Content.Load<Texture2D>("shuttle");
        }

        private int updates = 0;

        protected override void Update(GameTime gameTime)
        {
            
            
            bool enemy2Flag = false;

//            Clock.getClock().SetGameTime(gameTime);
            Clock.getClock().UpdateTime(gameTime);
            director.Update();
            canvas.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // spriteBatch.Begin();
            canvas.Draw();
            // DrawingTool.DrawCircle(spriteBatch, new Vector2(100,100), 30, Color.Red, 9);
            // DrawingTool.DrawLineSegment(spriteBatch, new Vector2(1,1), new Vector2(100,100), Color.White, 5);
            // DrawingTool.DrawRectangle(spriteBatch, new Rectangle(50, 50, 100, 300), Color.Red, 5);
            // spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}