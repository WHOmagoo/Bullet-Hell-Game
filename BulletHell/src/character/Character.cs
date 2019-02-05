﻿using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Character : Entity
    {
        private int healthPoints;
        private bool hitBox;    // bool representing whether or not to display hitbox
        //protected Gun gunEquipped;  //need Gun class

        public Character(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(canvas,texture,startLocation)
        {
            hitBox = false;
            healthPoints = 1000;    // just chose a random value of 1000 for now (value may depend on which character)
        }

        public Character(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        {
            hitBox = false;
            healthPoints = 1000;    // (same as above)
        }

        public void Update(GameTime theGameTime, Vector2 speed, Vector2 direction)
        {
            Location += direction * speed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }
        
        /*public void Shoot()
        {
            //need gun class   TODO
            gun.Shoot();
        }*/

        public bool ShowHitbox
        {
            get { return hitBox; }
            set { hitBox = value; }

        }

        /*public OnHit(Bullet bullet)
        {
            //need bullet class     TODO
        }*/


    }
}