using System.Collections.Generic;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public abstract class BulletFactory
    {
        public abstract List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0);
    }
}