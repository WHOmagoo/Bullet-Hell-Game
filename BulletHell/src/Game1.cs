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

        int midbossFlag = 0;        //needed because we only want to create midboss once
        int finalbossFlag = 0;      //needed because we only want to create finalboss once
        int shootPatternFlag = 0;   //needed to keep track of which shooting pattern we should be on

        private Enemy enemy;
        private MidBoss midboss;
        private FinalBoss finalboss;

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
            Vector2 loc = new Vector2(20, 20);
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

            if (gameTime.TotalGameTime.Seconds > 48 && midbossFlag == 0)
            {
                midbossFlag = 1;
                Texture2D midBossTexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));
                midboss = new MidBoss(canvas, midBossTexture, new Rectangle(100, 5, 100, 100));
                midboss.movePattern();
            }
            if (gameTime.TotalGameTime.TotalSeconds > 90 && finalbossFlag == 0)
                {
                    finalbossFlag = 1;
                    Texture2D finalBossTexture = Texture2D.FromStream(GraphicsDevice,
                    new FileStream("Content/sprites/finalboss.png", FileMode.Open));
                    finalboss = new FinalBoss(canvas, finalBossTexture, new Rectangle(100, 5, 100, 100));
                    finalboss.movePattern();
                }
            if (midbossFlag == 1)
            {
                midboss.Update();
         
                if(gameTime.TotalGameTime.TotalSeconds < 75)
                {   //we don't want bullets to continue shooting when the enemy has left the screen
                    //(enemy leaves screen at 75 seconds)
                    midboss.Shoot();
                }

            }
            if (finalbossFlag == 1)
            {
                finalboss.Update();
                //control different shooting directions:
                if (gameTime.TotalGameTime.TotalSeconds <120 && shootPatternFlag ==0)
                {
                    shootPatternFlag = 1;
                    finalboss.shootMethod1();
                }
                else if(gameTime.TotalGameTime.TotalSeconds >120 && gameTime.TotalGameTime.TotalSeconds <130 && shootPatternFlag ==1)
                {
                    shootPatternFlag = 2;
                    finalboss.shootMethod2();
                }
                else if (gameTime.TotalGameTime.TotalSeconds >130 && gameTime.TotalGameTime.TotalSeconds < 150 && shootPatternFlag ==2)
                {
                    shootPatternFlag = 3;
                    finalboss.shootMethod3();
                }
                else if (gameTime.TotalGameTime.TotalSeconds > 150 && gameTime.TotalGameTime.TotalSeconds < 162 && shootPatternFlag == 3)
                {
                    shootPatternFlag = 4;
                    finalboss.shootMethod4();
                }
                if (gameTime.TotalGameTime.TotalSeconds < 162)
                {
                    finalboss.Shoot();
                }
            }

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