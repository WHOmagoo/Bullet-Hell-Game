using System;
using Microsoft.Xna.Framework;

namespace BulletHellTests.GameEngine
{
    public class SpiralLocationEquation : ILocationEquation
    {
        private float x;
        private float y;
        private float a; //starting angle of spiral
        private float b; //outward spiral scalar. ie 
        private float speed;

        /// <summary>
        ///     Creates a new LinearEquation using parametric form
        /// </summary>
        /// <param name="angle">An angle, measured in radians. </param>
        /// <param name="speed">The speed at which the object should move in units per millisecond</param>
        public SpiralLocationEquation(float a, float b, float speed)
        {
            this.a = a;
            this.b = b;
            this.speed = speed;
        }
        
        /// <summary>   
        ///     Returns the location that an object should be at after a certain amount of ticks have occured
        /// </summary>
        /// <param name="ticksElapsed">An integer number of the amount of ticks that have happened since first
        ///     initiating the movement of the object</param>
        /// <returns> A tuple of the new location of an object on the specified equation after the amount of ticks that have elapsed</returns>
        public Vector2 GetLocation(long ticksElapsed)
        {
            long t = ticksElapsed;
            x = (float)((b + (float)t) * Math.Cos((double)((float)t + a)));
            y = (float)((b + (float)t) * Math.Sin((double)((float)t + a)));
            return new Vector2(x * speed, y * speed);
        }
    }
}