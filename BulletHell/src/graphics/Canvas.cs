using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Graphics
{
    public class Canvas
    {
        private SpriteBatch spriteBatch;

        //TODO: right now this is using a singleton but we should probably update this to use observer pattern

        private static Canvas canvas;
        private LinkedList<Entity> entities;

        private Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.entities = new LinkedList<Entity>();
        }
        public static bool makeCanvas(SpriteBatch spriteBatch)
        {
            bool madeNewCanvas = ReferenceEquals(canvas, null);

            if (madeNewCanvas)
            {
                canvas = new Canvas(spriteBatch);
                return true;
            }
            else
            {
                throw new Exception("Canvas singleton instance is already initialized");
            }
        }

        public static Canvas getCanvas()
        {
            if (!ReferenceEquals(canvas, null))
            {
                return canvas;
            }
            throw new NullReferenceException("The canvas was not constructed yet. Call makeCanvas first.");
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
            entities.AddLast(entity);
        }
        public void RemoveFromDrawList(Entity entity)
        {
            entities.Remove(entity);
        }


        //TODO: fix this bodge
        // public void Update()
        // {
        //     foreach (var entity in entities)
        //     {
        //         if (entity.GetType() == typeof(Bullet))
        //         {
        //             Bullet b = (Bullet)entity;

        //             b.Update();
        //         }
        //     }
        // }
        public Rectangle GetBounds()
        {
            return spriteBatch.GraphicsDevice.Viewport.Bounds;
        }
    }
}