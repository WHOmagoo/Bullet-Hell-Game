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

        public Gun(float delay, Texture2D texture, BulletFactory factory, TEAM team, double fireAngleOffset = Math.PI / 2)
        {
            this.bulletTexture = texture;
            this.tickFireDelay = (long) (delay * 1000);
            this.fireShape = factory;
            this.team = team;
            this.fireAngleOffset = fireAngleOffset;
        }

        public Gun(Gun g)
        {
            this.bulletTexture = g.bulletTexture;
            this.tickFireDelay = g.tickFireDelay;
            this.fireShape = g.fireShape;
            this.team = g.team;
            this.fireAngleOffset = g.fireAngleOffset;
        }
        public virtual void Shoot(Vector2 location)
        {
            if (canShoot())
            {
                OnShoot(fireShape.makeBullets(location, bulletTexture, team, fireAngleOffset));
                wasShot();
            }
        }

        public virtual void UpdateLoc(Vector2 loc)
        {
        }

        public Gun Copy(){
            return new Gun(this.tickFireDelay / 1000, this.bulletTexture, fireShape, team, fireAngleOffset);
        }

        public void Update() { }

        public virtual void wasShot()
        {
            lastShotTick = Clock.getClock().getTime();
        }

        public bool canShoot()
        {
            return lastShotTick + tickFireDelay < Clock.getClock().getTime() && !ReferenceEquals(null, this.fireShape);
        }
        
        protected virtual void OnShoot(List<Bullet> bulletsCreated)
        {
            if(bulletsCreated != null)
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

