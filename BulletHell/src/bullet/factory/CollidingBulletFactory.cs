using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class CollidingBulletFactory : BulletFactory
    {
        private BulletFactory factory;

        public CollidingBulletFactory(BulletFactory factory)
        {
            this.factory = factory;
        }

        public List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            List<Bullet> result = new List<Bullet>();
            
            List<Bullet> createdBullets = factory.makeBullets(location, bulletTexture, team, angleOffset);
                
            foreach(var b in createdBullets)
            {
                Bullet newBullet = new BulletCollidingBullet(b);
                    
                result.Add(newBullet);
                BHGame.CollisionManager.removeFromTeam(b, b.team);
                BHGame.CollisionManager.addToTeam(newBullet, TEAM.UNASSIGNED);
            }

            return result;
        }
    }
}