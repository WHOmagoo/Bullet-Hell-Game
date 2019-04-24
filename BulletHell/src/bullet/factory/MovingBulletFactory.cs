using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class MovingBulletFactory : BulletFactory
    {
        private Path path;
        private ILocationEquation pathLocationEquation;
        private BulletFactory bulletFactory;

        public MovingBulletFactory(BulletFactory bulletFactory, ILocationEquation pathToFollow)
        {
            this.pathLocationEquation = pathToFollow;
            this.path = new BasicPath(pathToFollow, Vector2.Zero, 0, 1);
            this.bulletFactory = bulletFactory;
        }

        public void ResetMovePath()
        {
            path.Reset();
        }

        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            Vector2 relLoc = path.UpdateLocation();
            Vector2 newLocation = location + relLoc;
            List<Bullet> created = bulletFactory.makeBullets(newLocation, bulletTexture, team, angleOffset); 
            return new List<Bullet>(created);
        }
    }
}