using System;
using System.Collections.Generic;
using BulletHell.gun;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;

namespace BulletHell.bullet
{
    public class SurroundBulletFactory : BulletFactory
    {
        private float weaponSpread;
        private float direction;
        private int amountOfBullets;
        private BulletFactory bulletsToSpawn;

        public SurroundBulletFactory(int amountOfBullets, float direction, float spread, BulletFactory bulletsToSpawn)
        {
            this.weaponSpread = spread;
            this.direction = direction;
            this.amountOfBullets = amountOfBullets;
            this.bulletsToSpawn = bulletsToSpawn;
        }
        
        public override List<Bullet> makeBullets(Vector2 location, TEAM team, double angleOffset = 0)
        {
            List<Bullet> result = new List<Bullet>();

            Bullet b;
            double increment = Math.PI / (amountOfBullets/2d);
            for (double i = 0; i < 2 * Math.PI; i += increment)
            {
                List<Bullet> bulletsCreated = bulletsToSpawn.makeBullets(location, team, angleOffset);

                result.AddRange(bulletsCreated);
                
//                Path p = new Path(fireShape, location, i);
//                b = new Bullet(damage, p, bulletTexture, team);
//                b.SetSize(20, 30);
//                b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0, 0), 20, 30);
//                BHGame.CollisionManager.addToTeam(b, team);
//                result.Add(b);
            }

            return result;
        }
    }
}