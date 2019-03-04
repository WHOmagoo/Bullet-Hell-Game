using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHellTests.GameEngine
{
    public abstract class Gun
    {

        protected int damage;
        //Bullet bulletNegative;
        protected ILocationEquation fireShape;
        private readonly long tickFireDelay;
        private long lastShotTick;
        protected Boolean friendly;
        protected Texture2D bulletTexture;

        public Gun(int damage, ILocationEquation shape, Texture2D texture, long delay, bool friend){
            this.damage = damage;
            fireShape = shape;
            tickFireDelay = delay;
            lastShotTick = 0;
            friendly = friend;
            bulletTexture = texture;
        }

        public abstract void shoot(Vector2 location);
        
        public virtual void wasShot()
        {
            lastShotTick = Clock.getClock().getTime();
        }

        public bool canShoot()
        {
            return lastShotTick + tickFireDelay < Clock.getClock().getTime();
        }
    }

}