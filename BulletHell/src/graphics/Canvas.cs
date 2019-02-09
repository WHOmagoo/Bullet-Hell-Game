using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Canvas
    {
        private LinkedList<Entity> entities;
        private SpriteBatch spriteBatch;

        //TODO right now this is using a singleton but we should probably update this to use observer pattern

        private static Canvas canvas;

        public static bool makeCanvas(SpriteBatch spriteBatch)
        {
            bool madeNewCanvas = ReferenceEquals(canvas, null);

            if (madeNewCanvas)
            {
                canvas = new Canvas(spriteBatch);
            }

            return true;
        }

        public static Canvas getCanvas()
        {
            if (!ReferenceEquals(canvas, null))
            {
                return canvas;
            }

            throw new NullReferenceException("The canvas was not constructed yet");
        }

        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.entities = new LinkedList<Entity>();
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


        //TODO fix this bodge
        public void Update()
        {
            foreach (var entity in entities)
            {
                if (entity.GetType() == typeof(Bullet))
                {
                    Bullet b = (Bullet)entity;

                    b.Update();
                }
            }
        }
    }
}
