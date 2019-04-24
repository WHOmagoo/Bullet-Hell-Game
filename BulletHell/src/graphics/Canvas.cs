using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BulletHell.background;
using BulletHell.bullet;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.gun;
using BulletHell.Pickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.director;

// using BulletHell.director;


namespace BulletHell.graphics
{
    public class Canvas
    {
        private SpriteBatch spriteBatch;
        private LinkedList<Entity> entities;
        private LinkedList<Entity> enqueuBuf;
        private LinkedList<Entity> dequeuBuf;
        Background b1;
        Background b2;
        string message;
        bool hasMessage;
        static SpriteFont _font;

        LifePickup lp;

        public event EventHandler PlayerDeathHandler;
        public event EventHandler EnemyDeathHandler;

        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            
            
            Texture2D scrolling2 = Texture2D.FromStream(spriteBatch.GraphicsDevice,
                new FileStream("Content/sprites/star.png", FileMode.Open));
            Texture2D scrolling1 = Texture2D.FromStream(spriteBatch.GraphicsDevice,
                new FileStream("Content/sprites/star2.png", FileMode.Open));
            /*
            Texture2D mushtext = Texture2D.FromStream(spriteBatch.GraphicsDevice, new FileStream("Content/sprites/mushroom-1up.png", FileMode.Open));
            this.lp = new LifePickup(mushtext, new Vector2(200, 200), 80, 80);
            this.lp.Hitbox = new CollidingCircle(lp.Location, new Vector2(lp.Rect.Width / 2, lp.Rect.Height / 2), lp.Rect.Width / 2);
            */
            this.b1 = new Background(scrolling1, new Vector2(0, 0));
            this.b2 = new Background(scrolling2, new Vector2(700, 0));

            this.entities = new LinkedList<Entity>();
            this.dequeuBuf = new LinkedList<Entity>();
            this.enqueuBuf = new LinkedList<Entity>();
            this.hasMessage = false;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            b1.Draw(spriteBatch);
           
            b2.Draw(spriteBatch);
            foreach (Entity e in entities)
            {
                if(e is Player)
                {
                    Player x = e as Player;                    
                    if(x.drawSp)
                    {
                        x.Draw(spriteBatch);
                    }

                }
                else
                {
                    
                    e.Draw(spriteBatch);
                }
                //e.Draw(spriteBatch);
            }
            if (hasMessage)
            {
                spriteBatch.DrawString(_font, message, new Vector2(GetBounds().Height, GetBounds().Width), Color.White);
            }

            spriteBatch.End();
        }
        
        
        public void AddToDrawList(Entity entity)
        {
            if (!entities.Contains(entity) && !enqueuBuf.Contains(entity))
            {
                enqueuBuf.AddLast(entity);
            }
            else
            {    
                Console.WriteLine("Duplicate added to drawlist" + entity);
            }
        }

        public void RemoveFromDrawList(Entity entity)
        {
            dequeuBuf.AddLast(entity);
            entity = null;
        }


        public void Update()
        {
            b1.Update(0, -1);
            b2.Update(0, -1);

            foreach (var entity in entities)
            {
                //need to change so bullets delete from top too
                if(entity is Bullet || entity is Enemy)
                {
                    if (entity.Location.Y > 500)
                    {
                        RemoveFromDrawList(entity);
                        
                    }
                    if(entity.Location.X > 785 || entity.Location.X < -100)
                    {
                        RemoveFromDrawList(entity);
                    }
                    /*
                    if (entity is Bullet)
                    {
                        if (entity.Location.Y < 0)
                        {
                            RemoveFromDrawList(entity);
                        }
                    } */
                }
                
                
                updateEntity(entity);
            }

            while (enqueuBuf.First != null)
            {
                updateEntity(enqueuBuf.First.Value);
                entities.AddLast(enqueuBuf.First.Value);
                enqueuBuf.RemoveFirst();
            }

            while (dequeuBuf.First != null)
            {
                entities.Remove(dequeuBuf.First.Value);
                dequeuBuf.RemoveFirst();
            }
        }

        private void updateEntity(Entity entity)
        {
            GameObject cur = entity as GameObject;
            if (cur != null)
            {

                cur.Update();
                    
                Enemy enemy = cur as Enemy;

                enemy?.Shoot();
            }
        }
        
        
        public Rectangle GetBounds()
        {
            return spriteBatch.GraphicsDevice.Viewport.Bounds;
        }

        public void OnGunShot(object sender, BulletsCreatedEventArgs bullets)
        {
            foreach (Bullet bullet in bullets.Bullets)
            {
                AddToDrawList(bullet);
            }
        }

        public void OnWeaponChange(object sender, PropertyChangedEventArgs e)
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


        public void OnPlayerDeath(object sender, EventArgs e)
        {
            PlayerDeathHandler?.Invoke(sender, e);
        }


  
        public static void SetFont(SpriteFont font)
        {
            _font = font;
        }

        public void SetMessage(string message)
        {
            this.message = message;
            hasMessage = true;
        }

        public void TurnOffMessage()
        {
            hasMessage = false;
        }


    }
}