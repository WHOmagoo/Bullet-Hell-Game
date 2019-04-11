using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    internal class VectorRotation
    {
        public static Vector2 RotateVector(double angle, Vector2 vector)
        {
            float x = vector.X;
            float y = vector.Y;

            double newX = x * Math.Cos(angle) - y * Math.Sin(angle);
            double newY = - x * Math.Sin(angle) + y * Math.Cos(angle);
            
            return new Vector2((float) Math.Round(newX), (float) Math.Round(newY));

        }
    }
}