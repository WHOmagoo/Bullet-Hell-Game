using System.Collections.Generic;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public abstract class BulletFactory
    {
        public abstract List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0);

        protected void prepareBullet(Bullet bullet, TEAM team)
        {
            if(team == TEAM.ENEMY)
            {
                bullet.SetSize(30, 30);
                bullet.Hitbox = new CollidingCircle(bullet.Location, new Vector2(15, 15), 15);
            }
            else if(team == TEAM.FRIENDLY)
            {
                bullet.SetSize(20, 30);
                bullet.Hitbox = new CollidingRectangle(bullet.Location, new Vector2(0, 0), 20, 30);
            }
            else
            {
                throw new System.Exception("invalid team for preparing bullet");
            }


        }
    }
}