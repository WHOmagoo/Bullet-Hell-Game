using System;
using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gun
{
    public class Gun
    {

        protected int damage;
        //Bullet bulletNegative;
        protected BulletFactory fireShape;
        private readonly long tickFireDelay;
        private long lastShotTick;
        protected TEAM team;
        protected Texture2D bulletTexture;
        protected double fireAngleOffset;
        public event EventHandler<BulletsCreatedEventArgs> GunShotHandler;

        public Gun(long delay, Texture2D texture, BulletFactory factory, TEAM team, double fireAngleOffset = Math.PI / 2)
        {
            this.bulletTexture = texture;
            this.tickFireDelay = delay * 1000;
            this.fireShape = factory;
            this.team = team;
            this.fireAngleOffset = fireAngleOffset;
        }
        public void Shoot(Vector2 location)
        {
            if (canShoot())
            {
                OnShoot(fireShape.makeBullets(location, bulletTexture, team, fireAngleOffset));
                wasShot();
            }
        }


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

