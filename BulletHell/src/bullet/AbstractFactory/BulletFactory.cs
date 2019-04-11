using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.AbstractFactory
{
    public abstract class BulletFactory
    {
        public abstract List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0);
    }
}