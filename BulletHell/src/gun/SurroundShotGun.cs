using System;
using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class SurroundShotGun : Gun
    {

        private float weaponSpread;
        private float direction;
        private int amountOfBullets;

        public SurroundShotGun(int amountOfBullets, float direction, float spread, int damage, ILocationEquation shape, Texture2D texture, int delay, TEAM team) : base(damage, shape, texture, delay, team)
        {
            this.weaponSpread = spread;
            this.direction = direction;
            this.amountOfBullets = amountOfBullets;
        }

        public override void Shoot(Vector2 location)
        {
            if (canShoot())
            {
                var bullets = makeBullets(location);
                base.wasShot();
                OnShoot(bullets);
            }
        }

        private List<Bullet> makeBullets(Vector2 location)
        {
            List<Bullet> result = new List<Bullet>();

            Bullet b;
            double increment = Math.PI / (amountOfBullets/2d);
            for (double i = 0; i < 2 * Math.PI; i += increment)
            {
                Path p = new Path(fireShape, location, i);
                b = new Bullet(damage, p, bulletTexture, team);
                b.SetSize(20, 30);
                b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0, 0), 20, 30);
                BHGame.CollisionManager.addToTeam(b, team);
                result.Add(b);
            }

            return result;
        }
    }

}