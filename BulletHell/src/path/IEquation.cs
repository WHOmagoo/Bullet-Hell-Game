using System;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public interface IEquation
    {
        Vector2 updateLocation(int ticksElapsed);
    }
}