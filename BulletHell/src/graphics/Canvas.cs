using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Canvas
    {
        private static Canvas canvasInstance;
        private SpriteBatch spriteBatch;
        private LinkedList<Entity> entities;
        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.entities = new LinkedList<Entity>();
        }
        public static void initializeInstance(SpriteBatch spriteBatch) {
            if(canvasInstance != null)
                throw new Exception("Canvas singleton instance is already initialized");
            else {
                Canvas.canvasInstance = new Canvas(spriteBatch);
            }
        }
        public static Canvas getInstance() {
            if (canvasInstance == null)
                throw new Exception("Canvas singleton instance must be initialized with 'initializeInstance'");
            return canvasInstance;
        }
        public void Draw()
        {
            spriteBatch.Begin();
            foreach (Entity e in entities) {
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
    }
}