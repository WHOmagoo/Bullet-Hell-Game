using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class LinearPath : Path
    {
        private double xGrowRate;
        private double yGrowRate;

        /// <summary>
        ///     Creates a new LinearPath using parametric form
        /// </summary>
        /// <param name="angle">An angle, measured in radians. </param>
        /// <param name="speed">The speed at which the object should move in units per tick</param>
        public LinearPath(double angle, double speed)
        {
            xGrowRate = Math.Cos(angle) * speed;
            yGrowRate = Math.Sin(angle) * speed;
        }
        
        /// <summary>
        ///     Returns the location that an object should be at after a certain amount of ticks have occured
        /// </summary>
        /// <param name="ticksElapsed">An integer number of the amount of ticks that have happened since first
        ///     initiating the movement of the object</param>
        /// <returns> A tuple of the new location of an object on the specified path after the amount of ticks that have elapsed</returns>
        public override Tuple<double, double> updateLocation(int ticksElapsed)
        {
            return new Tuple<double, double>(ticksElapsed * xGrowRate, yGrowRate * ticksElapsed);
        }
    }
}