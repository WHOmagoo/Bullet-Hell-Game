using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class SpiralLocationEquation : ILocationEquation
    {
        private double angularVelocity;
        private double spiralGrowthRate;
        private double spiralOffset;

        /// <summary>
        ///     Create a spiral location equation, the item will spiral out from where it started using a fixed angular velocity
        ///     and fixed growth rate. A low spiral growth rate would best used for guns when enemies that are centered on the screen.
        ///     A larger spiral growth rate can be used on weapons for enemies that are moving because the bullets will be
        ///     farther apart and the player can thread the gaps between bullets.
        /// </summary>
        /// <param name="angularVelocity">The angular velocity of the item in radians per second </param>
        /// <param name="spiralGrowthRate">The growth rate of the circle that our time will trace in pixels per second</param>
        /// <param name="spiralOffset">An offset to make growth at the beginning go faster </param>
        public SpiralLocationEquation(double angularVelocity, double spiralGrowthRate, double spiralOffset = 0)
        {
            this.angularVelocity = angularVelocity;
            this.spiralGrowthRate = spiralGrowthRate;
            this.spiralOffset = spiralOffset;
        }
        
        /// <summary>   
        ///     Returns the location that an object should be at after a certain amount of ticks have occured
        /// </summary>
        /// <param name="ticksElapsed">An integer number of the amount of ticks that have happened since first
        ///     initiating the movement of the object</param>
        /// <returns> A tuple of the new location of an object on the specified equation after the amount of ticks that have elapsed</returns>
        public Vector2 GetLocation(long ticksElapsed)
        {
            double distanceFromCenter = spiralGrowthRate * (ticksElapsed / 1000.0) + spiralOffset;
            double angle = ticksElapsed * angularVelocity / 1000.0;

            float x = (float) Math.Round(Math.Cos(angle) * distanceFromCenter);
            float y = (float) Math.Round(Math.Sin(angle) * distanceFromCenter);
            
            return new Vector2(x, y);

        }
    }
}