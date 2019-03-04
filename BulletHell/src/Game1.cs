using System;
using System.IO;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BulletHell.Graphics;

namespace BulletHell
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Canvas canvas;
        SpriteBatch spriteBatch;
        // Texture2D shuttle;
        Player player;

        int midbossFlag = 0;        //needed because we only want to create midboss once
        int finalbossFlag = 0;      //needed because we only want to create finalboss once
        int shootPatternFlag = 0;   //needed to keep track of which shooting pattern we should be on

        private Enemy enemy1;
        private Enemy enemy2;
        private MidBoss midboss;
        private FinalBoss finalboss;
        //Textures
        private Texture2D enemyBTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Canvas.makeCanvas(spriteBatch);
            canvas = Canvas.getCanvas();
            //Load textures
            Texture2D playerTexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/shuttle.png", FileMode.Open));

            Texture2D enemyATexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            enemyBTexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/enemyB.png", FileMode.Open));

            // fileStream.Dispose(); may need to do this for the filestreams made here in constructors
            GraphicsLoader.makeGraphicsLoader(GraphicsDevice);
            GraphicsLoader.getGraphicsLoader().setGraphicsTexture(new FileStream("Content/sprites/bullet.png", FileMode.Open));


            //Initialize characters
            player = new Player(canvas, playerTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - playerTexture.Width / 2, 300));
            player.SetSize(72, 100);
            enemy1 = new EnemyA(canvas, enemyATexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
            enemy2 = new EnemyB(canvas, enemyBTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, -100));
            enemy2.SetSize(100, 100);
            // enemy2 = new EnemyA(canvas, enemyBTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
            // enemy2.SetSize(100, 100);
            // Entity e2 = new Entity(canvas, texture, new Rectangle(100,300,20,20));
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

            Clock.getClock().SetGameTime(gameTime);

            Clock.getClock().Update();
            double seconds = gameTime.TotalGameTime.TotalSeconds;

            updates++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // canvas.Update();
            player.Update();
            // enemy1.Update();
            // enemy1.Shoot();
            // if (enemy2Flag)
            // {
            //     enemy2.Update();
            //     enemy2.Shoot();
            //     // enemy2.Move(new Vector2(1,1));
            // }

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