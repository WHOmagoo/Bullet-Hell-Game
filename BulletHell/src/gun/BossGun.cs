using System;
using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.path;

namespace BulletHell.gun
{
    public class BossGun : Gun
    {

        private float weaponSpread;
        private float direction;
        private int delay;
        BulletFactory bfactory;
        
        /*
            TODO: IMPLEMEMENT GUN TO FOLLOW SPECIFIC SHAPE/PATTERN EQUATION
        */
        public BossGun(int delay, BulletFactory factory, float direction, ILocationEquation shape, Texture2D texture, TEAM team) : base(delay, texture, factory, team)
        {
            this.delay = delay;
            this.bfactory = factory;
            this.direction = direction;
        }

        
    }

}