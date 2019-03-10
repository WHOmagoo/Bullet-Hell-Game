using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BulletHell.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public abstract class Gun
    {

        protected int damage;
        //Bullet bulletNegative;
        protected ILocationEquation fireShape;
        private readonly long tickFireDelay;
        private long lastShotTick;
        protected TEAM team;
        protected Texture2D bulletTexture;
        public event EventHandler<BulletsCreatedEventArgs> GunShotHandler;



        public Gun(int damage, ILocationEquation shape, Texture2D texture, long delay, TEAM team){
            this.damage = damage;
            fireShape = shape;
            tickFireDelay = delay;
            lastShotTick = 0;
            this.team = team;
            bulletTexture = texture;
        }

        public abstract void Shoot(Vector2 location);


        public virtual void wasShot()
        {
            lastShotTick = Clock.getClock().getTime();
        }

        public bool canShoot()
        {
            return lastShotTick + tickFireDelay < Clock.getClock().getTime();
        }
        
        protected virtual void OnShoot(List<Bullet> bulletsCreated)
        {
            GunShotHandler?.Invoke(this, new BulletsCreatedEventArgs(bulletsCreated));
        }

    }

    public class BulletsCreatedEventArgs : EventArgs
    {
        public List<Bullet> Bullets { get; }
    int damage;
        public BulletsCreatedEventArgs(List<Bullet> bullets)
        {
            this.Bullets = bullets;
        }
    }

}

