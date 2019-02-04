using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public interface IEquation
    {
        Vector2 updateLocation(int ticksElapsed);
    }
}