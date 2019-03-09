using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.director;
using BulletHell.GameEngine;

namespace BulletHell.Graphics
{
    public class Canvas
    {
        private SpriteBatch spriteBatch;

        //TODO: right now this is using a singleton but we should probably update this to use observer pattern
        
        private LinkedList<Entity> entities;
        private LinkedList<Entity> enqueuBuf;
        private LinkedList<Entity> dequeuBuf;

        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.entities = new LinkedList<Entity>();
            this.dequeuBuf = new LinkedList<Entity>();
            this.enqueuBuf = new LinkedList<Entity>();
        }

        public void Draw()
        {
            spriteBatch.Begin();
            foreach (Entity e in entities)
            {
                e.Draw(spriteBatch);
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
            {    int damage;
                Console.WriteLine("Duplicate added");
            }
        }
        public void RemoveFromDrawList(Entity entity)
        {
            dequeuBuf.AddLast(entity);
        }


        public void Update()
        {
            foreach (var entity in entities)
            {
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
    }
}