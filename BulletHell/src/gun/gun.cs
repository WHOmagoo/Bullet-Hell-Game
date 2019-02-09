using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public abstract class Gun
    {

        int damage;
        //Bullet bulletNegative;
        iLocationEquation fireShape;
        int tickFireDelay;
        int lastShotTick;
        Boolean friendly;
        Texture2D bulletTexture;

        Gun(int damage, iLocationEquation shape, Texture2d texture, int delay, bool friend){
            this.damage = damage;
            fireShape = shape;
            tickFireDelay = delay;
            lastShotTick = 0;
            friendly = friend;
            bulletTexture = texture;
        }
        public abstract void shoot(Vector2 location);
        private abstract Bullet makeBullet();
    }

}