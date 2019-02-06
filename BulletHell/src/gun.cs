using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine{
    public abstract class Gun{

        int damage;
        //Bullet bulletNegative;

        Gun(/*Bullet negative, */int damage){
            this.damage = damage;
            //bulletNegative = negative;
        }
        public void shoot();
        private abstract Bullet makeBullet();
    }

}