using System;
using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.path;
using BulletHell.ObjectCreation;

namespace BulletHell.gun
{
    public class BossGun : Gun
    {

        private float weaponSpread;
        private float direction;
        private int delay;
        BulletFactory bfactory;
        SpiralLocationEquation path;
        Vector2 _location;
        long time_elapsed;
        private long startTime;
        bool shooting;


        public virtual Vector2 Location
        {
            get { return _location; }
            set
            {
                _location = value;
            }
        }

        /*
            TODO: IMPLEMEMENT GUN TO FOLLOW SPECIFIC SHAPE/PATTERN EQUATION
        */
        public BossGun(Vector2 startingLoc, int delay, BulletFactory factory, float direction, ILocationEquation shape, Texture2D texture, TEAM team) : base(delay, texture, factory, team)
        {
            Location = startingLoc;
            this.delay = delay;
            this.bfactory = factory;
            this.direction = direction;
            base.fireAngleOffset = Math.PI/2;

            path = (SpiralLocationEquation)shape;
            shooting = false;


        }

        public override void Shoot(Vector2 location)
        {
            if (canShoot())
            {
                time_elapsed = Clock.getClock().getTime() - startTime;

                if (time_elapsed < 6000)
                {
                    Vector2 relLoc = path.GetLocation(time_elapsed);
                    Location = location + relLoc;
                    //OnShoot(fireShape.makeBullets(location+relLoc, bulletTexture, team, Location.Y));
                    OnShoot(fireShape.makeBullets(location + relLoc, bulletTexture, team, Math.PI));
                    wasShot();
                }
                else if (time_elapsed > 8000)
                    startTime = 8000;
            }
        }
        

    }

}