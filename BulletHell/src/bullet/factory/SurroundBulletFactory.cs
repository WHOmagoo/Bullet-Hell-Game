using System;
using System.Collections.Generic;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class SurroundBulletFactory : BulletFactory
    {
        private int amountOfBullets;
        private BulletFactory bulletsToSpawn;

        public SurroundBulletFactory(int amountOfBullets, BulletFactory bulletsToSpawn)
        {
            this.amountOfBullets = amountOfBullets;
            this.bulletsToSpawn = bulletsToSpawn;
        }
        
        public List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset)
        {
            List<Bullet> result = new List<Bullet>();

            Bullet b;
            double increment = Math.PI / (amountOfBullets/2d);
            for (double i = 0; i < 2 * Math.PI; i += increment)
            {
                List<Bullet> bulletsCreated = bulletsToSpawn.makeBullets(location, bulletTexture, team, i);

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