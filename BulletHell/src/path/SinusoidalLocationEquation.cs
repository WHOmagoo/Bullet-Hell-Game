using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class SinusoidalLocationEquation : ILocationEquation
    {
        private double angle;
        private double period;
        private double amplitude;
        private double dampening;

        /// <summary>
        ///     A sinusoidal function for movement
        /// </summary>
        /// <param name="angle">The angle at which the path should move</param>
        /// <param name="amplitude">The amplitude of the sin wave (how high does it go)</param>
        /// <param name="period">The period of the sin wave (how far does it go before a full cycle)</param>
        /// <param name="dampening">
        ///     The dampening of the sin wave. (How does the wave decrease over time).
        ///     Positive values closer to 0 will have less dampening, a value of 0 will have no dampening
        ///     Negative values will cause the wave to grow (use small numbers close to 0 for this)    
        ///  </param>
        public SinusoidalLocationEquation(double angle, double amplitude, double period, double dampening = 0)
        {
            this.angle = angle;
            this.period = period/2;
            this.amplitude = amplitude;
            this.dampening = dampening;
        }

        public Vector2 GetLocation(long ticksElapsed)
        {
            double y = amplitude * Math.Sin(Math.PI * ticksElapsed / period) * Math.Exp(dampening * ticksElapsed);

            return VectorRotation.RotateVector(angle, new Vector2(ticksElapsed, (float) Math.Round(y)));
        }
    }
}