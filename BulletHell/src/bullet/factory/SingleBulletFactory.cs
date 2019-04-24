using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class SingleBulletFactory : BulletFactory
    {
        private ILocationEquation path;
        
        public SingleBulletFactory(ILocationEquation pathToFollow)
        {
            this.path = pathToFollow;
        }

        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            Bullet created = new Bullet(1, new RotatedLocationEquation(path, angleOffset), bulletTexture, location, team);
            prepareBullet(created, team);
            BHGame.CollisionManager.addToTeam(created, team);
            return new List<Bullet>(new []{created});
        }
    }
}