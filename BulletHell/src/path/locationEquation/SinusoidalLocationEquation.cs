using System;
using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public class SinusoidalLocationEquation : ILocationEquation
    {
        private double period;
        private double amplitude;
        private double dampening;
        private double speed;

        /// <summary>
        ///     A sinusoidal function for movement
        /// </summary>
        /// <param name="speed">How many pixels do you move per second in the x direction</param>
        /// <param name="amplitude">The amplitude of the sin wave (how high does it go)</param>
        /// <param name="period">The period of the sin wave (how far does it go before a full cycle)</param>
        /// <param name="dampening">
        ///     The dampening of the sin wave. (How does the wave decrease over time).
        ///     Positive values closer to 0 will have less dampening, a value of 0 will have no dampening
        ///     Negative values will cause the wave to grow (use small numbers close to 0 for this)    
        ///  </param>
        public SinusoidalLocationEquation(double speed, double amplitude, double period, double dampening = 0)
        {
            this.speed = speed;
            this.period = period/2;
            this.amplitude = amplitude;
            this.dampening = dampening;
        }

        public Vector2 GetLocation(long ticksElapsed)
        {
            double x = speed * ticksElapsed / 1000;
            double y = amplitude * Math.Sin(Math.PI * x / period) * Math.Exp(- dampening * x);

            return new Vector2((float) Math.Round(x), (float) Math.Round(y));
        }
    }
}