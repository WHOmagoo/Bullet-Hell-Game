using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Canvas
    {
        private SpriteBatch spriteBatch;

        //TODO: right now this is using a singleton but we should probably update this to use observer pattern
        
        private LinkedList<GameObject> entities;
        private LinkedList<GameObject> toAdd;
        private LinkedList<GameObject> toRemove;

        public Canvas(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.entities = new LinkedList<GameObject>();
            this.toAdd = new LinkedList<GameObject>();
            this.toRemove = new LinkedList<GameObject>();
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
        
        
        public void AddToDrawList(GameObject entity)
        {
            if (!entities.Contains(entity) && !toAdd.Contains(entity))
            {    
                
                    toAdd.AddLast(entity);
            }
            else
            {
                Console.WriteLine("Duplicate added");
            }
        }
        public void RemoveFromDrawList(GameObject entity)
        {
            entities.Remove(entity);
        }

        public void Update()
        {
            while (toRemove.Count > 0)
            {
                entities.Remove(toRemove.First.Value);
                toRemove.RemoveFirst();
            }
            
            foreach (var entity in entities)
            {
                entity.Update();
            }

            while ( toAdd.Count > 0)
            {
                entities.AddLast(toAdd.First.Value);
                toAdd.First().Update();
                toAdd.RemoveFirst();
            }
        }
        
        
        public Rectangle GetBounds()
        {
            return spriteBatch.GraphicsDevice.Viewport.Bounds;
        }
    }
}