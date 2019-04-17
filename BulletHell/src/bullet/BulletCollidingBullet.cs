using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class BulletCollidingBullet : Bullet
    {

        private HashSet<Bullet> collidedWith = new HashSet<Bullet>();
        public override void onCollision(GameObject hitby)
        {
            Bullet bullet = hitby as Bullet;
            if (!ReferenceEquals(bullet, null) && !collidedWith.Contains(bullet))
            {
                bullet.setPath(new OffsetPath(this.pathToFollow, hitby.Location - Location));
                collidedWith.Add(bullet);
            }
        }

        public BulletCollidingBullet(Bullet b) : base(b.Damage, b.pathToFollow, b.texture, b.team)
        {
            this.Hitbox = b.Hitbox;
            this.team = b.team;
        }

        public BulletCollidingBullet(int damage, ILocationEquation locationEquation, Texture2D texture, Vector2 startLocation, TEAM team) : base(damage, locationEquation, texture, startLocation, team)
        {
        }

        public BulletCollidingBullet(int damage, Path path, Texture2D texture, TEAM team) : base(damage, path, texture, team)
        {
        }
    }
}