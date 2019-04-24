using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class BulletWaveWithCollidingBullet : BulletFactory
    {

        private bool onWave = true;

        private BulletFactory bulletWave;
        private BulletFactory collidingBullets;

        public BulletWaveWithCollidingBullet(BulletFactory bulletWave, BulletFactory collidingBullets)
        {
            this.bulletWave = bulletWave;
            this.collidingBullets = collidingBullets;
        }
        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            List<Bullet> result = new List<Bullet>();

            if (onWave)
            {
                 result = bulletWave.makeBullets(location, bulletTexture, team, angleOffset);
            }
            else
            {
                List<Bullet> createdBullets = collidingBullets.makeBullets(location, bulletTexture, team, angleOffset);
                
                foreach(var b in createdBullets)
                {
                    Bullet newBullet = new BulletCollidingBullet(b);
                    
                    result.Add(newBullet);
                    BHGame.CollisionManager.removeFromTeam(b, b.team);
                    BHGame.CollisionManager.addToTeam(newBullet, TEAM.UNASSIGNED);
                }
            }
            onWave = !onWave;

            return result;
        }
    }
}