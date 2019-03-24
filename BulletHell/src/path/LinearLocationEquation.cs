using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class LinearLocationEquation : ILocationEquation
    {
        private float xGrowRate;
        private float yGrowRate;

        /// <summary>
        ///     Creates a new LinearEquation using parametric form
        /// </summary>
        /// <param name="angle">An angle, measured in radians. </param>
        /// <param name="speed">The speed at which the object should move in units per millisecond</param>
        public LinearLocationEquation(double angle, double speed)
        {
            xGrowRate = (float) (Math.Cos(angle) * speed);
            yGrowRate = (float) (Math.Sin(angle) * speed);
        }
        
        /// <summary>
        ///     Returns the location that an object should be at after a certain amount of ticks have occured
        /// </summary>
        /// <param name="ticksElapsed">An integer number of the amount of ticks that have happened since first
        ///     initiating the movement of the object</param>
        /// <returns> A tuple of the new location of an object on the specified equation after the amount of ticks that have elapsed</returns>
        public Vector2 GetLocation(long ticksElapsed)
        {
            return new Vector2(ticksElapsed * xGrowRate, yGrowRate * ticksElapsed);
        }
    }
}