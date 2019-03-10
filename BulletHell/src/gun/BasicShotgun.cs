using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class BasicShotgun : Gun 
    {
        public BasicShotgun(int damage, ILocationEquation shape, Texture2D texture, int delay, TEAM team) : base(damage, shape, texture, delay, team)
        {
        }

        public override void Shoot(Vector2 location)
        {
            throw new NotImplementedException();
        }
    }

}