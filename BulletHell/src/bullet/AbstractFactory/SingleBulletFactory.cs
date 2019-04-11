using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.AbstractFactory
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
            return new List<Bullet>(new []{new Bullet(1, path, bulletTexture, location, team)});
        }
    }
}