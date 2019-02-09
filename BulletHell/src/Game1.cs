using System.IO;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletHell
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Canvas canvas;
        SpriteBatch spriteBatch;
        // Texture2D shuttle;
        Player player;

        private Enemy enemy;

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
            FileStream fileStream = new FileStream("Content/sprites/shuttle.png", FileMode.Open);
            Texture2D playerTexture = Texture2D.FromStream(GraphicsDevice, fileStream);

            Texture2D enemyATexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            GraphicsLoader.makeGraphicsLoader(GraphicsDevice);
            GraphicsLoader.getGraphicsLoader().setGraphicsTexture(new FileStream("Content/sprites/bullet.png", FileMode.Open));


            fileStream.Dispose();
            Vector2 loc = new Vector2(20,20);
            player = new Player(canvas, playerTexture, loc);
            enemy = new EnemyA(canvas, enemyATexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
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

            Clock.getClock().SetGameTime(gameTime);
            
            Clock.getClock().Update();
            
//            Console.WriteLine("{0}, Game Time Elapsed since last draw: {1}", updates, gameTime.ElapsedGameTime);
            updates++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            canvas.Update();
            
            player.Update();
            enemy.Update();
            enemy.Shoot();
            
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