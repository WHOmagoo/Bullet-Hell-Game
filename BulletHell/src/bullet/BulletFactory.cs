using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gun
{
    public abstract class BulletFactory
    {
        public abstract List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0);
    }
}