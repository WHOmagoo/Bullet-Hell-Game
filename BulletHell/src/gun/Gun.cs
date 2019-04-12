using System;
using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gun
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
        protected double fireAngleOffset;
        public event EventHandler<BulletsCreatedEventArgs> GunShotHandler;



        //FIXME: Group fireangle and fireshape to similar location to avoid confusion
        public Gun(int damage, ILocationEquation fireShape, Texture2D texture, long delay, TEAM team, double fireAngleOffset = 0){
            this.damage = damage;
            this.fireShape = fireShape;
            this.fireAngleOffset = fireAngleOffset;
            tickFireDelay = delay;
            lastShotTick = 0;
            this.team = team;
            bulletTexture = texture;
        }

        public Gun(long delay, BulletFactory factory)
        {
            //TODO change implementation of Gun to use this
            throw new NotImplementedException();
        }

        //TODO allow to set angle when shooting so that making a gun can be the same for both enemies and players
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

