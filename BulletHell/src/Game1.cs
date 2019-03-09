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

namespace BulletHell
{
    public class Game1 : Game
    {
        private GameDirector director;
        private Canvas canvas;

        private IGameFactory factory;


        public Game1(IGameFactory factory)
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.factory = factory;
        }

        protected override void Initialize()
        {
            GraphicsLoader.makeGraphicsLoader(GraphicsDevice);
            GraphicsLoader.getGraphicsLoader().setGraphicsTexture(new FileStream("Content/sprites/bullet.png", FileMode.Open));

            Tuple<GameDirector, Canvas> result = factory.makeGame(GraphicsDevice);
            director = result.Item1;
            canvas = result.Item2;
            
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
            canvas.Draw();
            base.Draw(gameTime);
        }
    }
}