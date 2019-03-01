using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            
            enemy1 = new EnemyA(enemyATexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
            enemy2 = new EnemyB(enemyBTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - 50, -100));
            player.PropertyChanged += OnWeaponChange;
            player.gunEquipped.GunShotHandler += OnGunShot;
            canvas.AddToDrawList(player);
            
            enemy1.PropertyChanged += OnWeaponChange;
            enemy1.gunEquipped.GunShotHandler += OnGunShot;
            canvas.AddToDrawList(enemy1);
            
            enemy2.SetSize(100, 100);
            enemy2.PropertyChanged += OnWeaponChange;
            enemy2.gunEquipped.GunShotHandler += OnGunShot;
            canvas.AddToDrawList(enemy2);
            // enemy2 = new EnemyA(canvas, enemyBTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
            // enemy2.SetSize(100, 100);
            // Entity e2 = new Entity(canvas, texture, new Rectangle(100,300,20,20));
            base.Initialize();
        }

        private void OnGunShot(object sender, BulletsCreatedEventArgs bullets)
        {
            foreach (Bullet bullet in bullets.Bullets)
            {
                canvas.AddToDrawList(bullet);
            }
        }

        private void OnWeaponChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Gun)))
            {
                Character c = sender as Character;

                if (!ReferenceEquals(c, null))
                {
                    c.gunEquipped.GunShotHandler += OnGunShot;
                }
            }
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
            // seconds = seconds * 2;

            //            Console.WriteLine("{0}, Game Time Elapsed since last draw: {1}", updates, gameTime.ElapsedGameTime);


            if (seconds > 16 && !enemy2Flag)
            {
                // enemy2 = new EnemyB(canvas, enemyBTexture, new Vector2(200, 100));
                // enemy2.SetSize(100, 100);
                // enemy2 = new EnemyB(canvas, enemyBTexture, new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, 0));
                // enemy2.SetSize(100, 100);
                enemy2Flag = true;
            }
            if (seconds > 44 && midbossFlag == 0)
            {
                midbossFlag = 1;
                Texture2D midBossTexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));
                // midboss = new MidBoss(canvas, midBossTexture, new Vector2(100,5), 100, 100);
                midboss = new MidBoss(midBossTexture, new Vector2(100, 5));
                midboss.SetSize(100, 100);
                midboss.movePattern();
                midboss.PropertyChanged += OnWeaponChange;
                midboss.gunEquipped.GunShotHandler += OnGunShot;
            }
            if (seconds > 80 && finalbossFlag == 0)
            {
                finalbossFlag = 1;
                Texture2D finalBossTexture = Texture2D.FromStream(GraphicsDevice,
                new FileStream("Content/sprites/finalboss.png", FileMode.Open));
                // finalboss = new FinalBoss(canvas, finalBossTexture, new Vector2(100, 5), 100, 100);
                finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5));
                finalboss.SetSize(100, 100);
                finalboss.movePattern();
                canvas.AddToDrawList(finalboss);
                finalboss.PropertyChanged += OnWeaponChange;
                finalboss.gunEquipped.GunShotHandler += OnGunShot;
            }
            if (midbossFlag == 1)
            {
                midboss.Update();

                if (seconds < 75)
                {   //we don't want bullets to continue shooting when the enemy has left the screen
                    //(enemy leaves screen at 75 seconds)
                    midboss.Shoot();
                }

            }
            if (finalbossFlag == 1)
            {
                finalboss.Update();
                //control different shooting directions:
                if (seconds < 120 && shootPatternFlag == 0)
                {
                    shootPatternFlag = 1;
                    finalboss.shootMethod1();
                }
                else if (seconds > 120 && seconds < 130 && shootPatternFlag == 1)
                {
                    shootPatternFlag = 2;
                    finalboss.shootMethod2();
                }
                else if (seconds > 130 && seconds < 150 && shootPatternFlag == 2)
                {
                    shootPatternFlag = 3;
                    finalboss.shootMethod3();
                }
                else if (seconds > 150 && seconds < 162 && shootPatternFlag == 3)
                {
                    shootPatternFlag = 4;
                    finalboss.shootMethod4();
                }
                if (seconds < 162)
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
            enemy1.Update();
            enemy1.Shoot();
            if (enemy2Flag)
            {
                enemy2.Update();
                enemy2.Shoot();
                // enemy2.Move(new Vector2(1,1));
            }

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