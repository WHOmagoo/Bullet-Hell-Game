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
        private int width;
        private int height;
        
        public SingleBulletFactory(ILocationEquation pathToFollow, int width = 20, int height = 30)
        {
            this.path = pathToFollow;
            this.width = width;
            this.height = height;
        }

        public List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            Bullet created = new Bullet(1, new RotatedLocationEquation(path, angleOffset), bulletTexture, location, team);
            created.SetSize(width, height);
            created.Hitbox = new CollidingRectangle(created.Location, new Vector2(0, 0), 20, 30);
            BHGame.CollisionManager.addToTeam(created, team);
            return new List<Bullet>(new []{created});
        }
    }
}