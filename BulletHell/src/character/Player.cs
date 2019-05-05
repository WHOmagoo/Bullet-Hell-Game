﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using BulletHell.bullet.factory;
using BulletHell.controls;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.GameEngine;
using BulletHell.Pickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    public class Player : Character
    {
        const string name2 = "download";
        const int startX = 125;
        const int startY = 245;

        internal Vector2 direction = Vector2.Zero;
        internal Vector2 speed = Vector2.Zero;
        private Vector2 respawnLocation;

        private Texture2D heartTexture;
        public Heart heart;
        public List<Heart> hearts;
        public int heartStartLoc;   //needed for placement of the hearts onto the screen

        Canvas canvas;
        public int slow = 0;

        public event EventHandler DeathEvent;

        public Player(Canvas canvas, Texture2D texture, Vector2 startLocation, Controller controller, Texture2D heart_texture) : base(texture, startLocation)
        {
            //invulnerable = true;
            this.respawnLocation = startLocation;
            this.canvas = canvas;
            InputControl.AssignPlayer(this);
            // gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), 
            //     GraphicsLoader.getGraphicsLoader().getTexture("bullet"), 500, TEAM.FRIENDLY);
            
            // gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, true);
            
            healthPoints = 5;      //player lives

            MakeHearts(heart_texture);
            AddHearts(healthPoints);
            gunEquipped = new Gun(1, GraphicsLoader.getGraphicsLoader().getTexture("player-bullet"), BulletFactoryFactory.make("basic"), TEAM.FRIENDLY, -Math.PI / 2);

            subscribeToController(controller);
        }

        private void subscribeToController(Controller controller)
        {
            controller.OnLeft += goLeft;
            controller.OnRight += goRight;
            controller.OnUp += goUp;
            controller.OnShoot += Shoot;
            controller.OnSlow += goSlow;
            controller.OnFast += goFast;
            controller.OnDown += goDown;
            controller.OnCheat += onCheat;
        }

        private void onCheat(object sender, EventArgs e)
        {
            
            invulnerable = !invulnerable;
            validEndInvulnerability = !invulnerable;
        }

        public void MakeHearts(Texture2D heart_texture)
        {
            heartTexture = heart_texture;
            hearts = new List<Heart>();
            heartStartLoc = 730;
        }

        public void AddHearts(int numAdded)
        {
            int i = 0;
            
            for (i = 0; i < numAdded; i++)
            {
                heart = new Heart(heartTexture, new Vector2(heartStartLoc, 6), 50, 50);
                hearts.Add(heart);
                canvas.AddToDrawList(heart);
                heartStartLoc -= 50;
            }

        }

        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public int Lives
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }

        public override void Update()
        {
            base.Update();
            float timeE = 10 * Clock.getClock().getTimeSinceLastUpdate();
            float scale = 0.5F;
            float scale2 = 2.0F;

            //timeE = timeE * scale * slow;
            if (slow == 1) timeE *= scale;
            if (slow == 2) timeE *= scale2;

            if (Rect.X + Rect.Width > canvas.GetBounds().Width)
                Location = new Vector2(canvas.GetBounds().Width - Rect.Width - 1, Location.Y);
            if (Rect.Y + Rect.Height > canvas.GetBounds().Height)
                Location = new Vector2(Location.X, canvas.GetBounds().Height - Rect.Height - 1);

            if (Location.X < 0) Location = new Vector2(1, Location.Y);
            if (Location.Y < 0) Location = new Vector2(Location.X, 1);
            else
            {
                Location += direction * speed * timeE  * 2 / 50000;
            }
            
            direction = Vector2.Zero;
            speed = Vector2.Zero;

            base.Update();
        }

        private void goLeft(object sender, EventArgs e)
        {
            InputControl.MoveLeft();
        }

        private void goRight(object sender, EventArgs e)
        {
            InputControl.MoveRight();
        }

        private void goUp(object sender, EventArgs e)
        {
            InputControl.MoveUp();
        }

        private void goDown(object sender, EventArgs e)
        {
            InputControl.MoveDown();
        }

        private void Shoot(object sender, EventArgs e)
        {
            gunEquipped.Shoot(Location);
        }

        private void goSlow(object sender, EventArgs e)
        {
            slow = 1;
        }

        private void goFast(object sender, EventArgs e)
        {
            slow = 0;
        }

        public override void onCollision(GameObject hitby)
        {
            // Console.WriteLine("hitby: " + hitby);
            if (!invulnerable && !(hitby is Pickup) && hitby.team != team)
            {
                startInvulnerability();
                TakeDamage(1);
                // base.onCollision(hitby);
                Location = respawnLocation;
            }
        }

        private Thread t;
        private bool validEndInvulnerability = true;
        public volatile bool invulnerable = false;
        public bool drawSp = true;
        public System.Timers.Timer myT = new System.Timers.Timer();
   
        private void myEv(object source, ElapsedEventArgs e)
        {
            drawSp = !drawSp;
        }


        public void startInvulnerability()
        {
            if (invulnerable)
            {
                try
                {
                    t.Abort();
                }
                catch (Exception unused)
                {
                    
                }
            }
            
            invulnerable = true;
            t = new Thread(invulnerableRunner);
            t.Start();
        }
        
        //create new timer on every collision to make player "blink"
        private void newTimer()
        {
            myT = new System.Timers.Timer();
            myT.Elapsed += new ElapsedEventHandler(myEv);
            myT.AutoReset = true;
            myT.Interval = 100; //can change for "blink" interval
            myT.Start();
        }

        private void invulnerableRunner()
        {
            Console.WriteLine("Invulnerable");
            // invulnerable = true;
            newTimer();
            Thread.Sleep(2000);
            if (validEndInvulnerability)
            {
                invulnerable = false;
                Console.WriteLine("Can now take damage again");
            }
            myT.Dispose();
            drawSp = true;
        }

        protected override void TakeDamage(int damage)
        {
            int i = 1;
            //while (i <= damage)
            //{
            //    healthPoints -= 1;
            //    if (healthPoints >= 0)
            //    {
            //        BHGame.Canvas.RemoveFromDrawList(hearts[hearts.Count - 1]);
            //        hearts.Remove(hearts[hearts.Count - 1]);
            //        heartStartLoc += 50;    //in case we need to add hearts later when a mushroom is picked up
            //    }
            //    i++;
            //}
            healthPoints -= 1;
            BHGame.Canvas.RemoveFromDrawList(hearts[hearts.Count - 1]);
            hearts.Remove(hearts[hearts.Count - 1]);
            heartStartLoc += 50;    //in case we need to add hearts later when a mushroom is picked up


            if (healthPoints <= 0)
            {
                CreateDeathEvent();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isSpriteVisible)
                base.Draw(spriteBatch);
            if (isHitboxVisible)
                hitbox?.DrawHitbox(spriteBatch, Color.Red, 1);
        }

        private void CreateDeathEvent()
        {
            DeathEvent?.Invoke(this, new EventArgs());
        }

        protected override void Die()
        {
            BHGame.CollisionManager.removeFromTeam(this, TEAM.FRIENDLY);
        }
    }
}
