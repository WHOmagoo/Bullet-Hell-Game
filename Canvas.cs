using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace test4
{
    public class Canvas
    {
        private LinkedList<Entity> entities;
        private SpriteBatch spriteBatch;
        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
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