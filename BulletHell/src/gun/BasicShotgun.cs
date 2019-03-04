using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class BasicShotgun : Gun 
    {
        public BasicShotgun(int damage, ILocationEquation shape, Texture2D texture, int delay, bool friend) : base(damage, shape, texture, delay, friend)
        {
        }

        public override void shoot(Vector2 location)
        {
            throw new NotImplementedException();
        }
    }

}