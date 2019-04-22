using System.Collections;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;

namespace BulletHell.ObjectCreation
{

    public class HitboxRepo 
    {
        private Hashtable hitboxTable;
        private static HitboxRepo hitboxRepo;

        private HitboxRepo()
        {
            hitboxTable = new Hashtable();
            this.LoadAllHitboxes();
        }

        public static HitboxRepo getHitboxRepo()
        {
            if(hitboxRepo == null)
                hitboxRepo = new HitboxRepo();
                // throw new System.ArgumentException("Must make HitboxRepo first");
            return hitboxRepo;
        }

        public Hitbox getHitbox(string hitboxName)
        {
            return (Hitbox)hitboxTable[hitboxName];
        }
        //Depreciated

        public bool addHitbox(string name, Hitbox hitbox)
        {
            try
            {
                hitboxTable.Add(name, hitbox);
                return true;
            }
            catch
            {
                //Already in table
                return false;
            }
        }

        private void LoadAllHitboxes()
        {
            addHitbox("enemyA", new CollidingRectangle(Vector2.Zero, Vector2.Zero, 100,75));
            addHitbox("enemyB", new CollidingRectangle(Vector2.Zero, new Vector2(46,30), 390,600));
            addHitbox("enemyC", new CollidingCircle(Vector2.Zero, new Vector2(474,464), 496));

            // addTexture("player", "Content/sprites/shuttle.png");
            // addTexture("enemyA", "Content/sprites/enemyA.png");
            // addTexture("enemyB", "Content/sprites/white-ghost.png");
            // addTexture("enemyC", "Content/sprites/octopus.png");
            // addTexture("midBoss", "Content/sprites/midboss.png");
            // addTexture("finalBoss", "Content/sprites/finalboss.png");
            // addTexture("heart", "Content/sprites/heart.png");
            // addTexture("bullet", "Content/sprites/bullet.png");
        }
    }
}