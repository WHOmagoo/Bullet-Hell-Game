using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class ZigZag : ILocationEquation
    {
        private LinearLocationEquation zig, zag;
        private long timeToZig, timeToZag;
        private bool wasZig;
        private Vector2 lastZigLoc;
        private Vector2 lastPivotLoc;
        
        
        /// <summary>
        ///     Create a new ZigZag equation that zigs and zags, it moves on one equation for an amount of time and
        ///     then on the other one for an amount of time
        /// </summary>
        /// <param name="zig"> The linear equation to follow while 'zigging', this defines the angle and the speed at which the object will move independent of zag</param>
        /// <param name="timeToZig">This defines how long the object will use the zig equation for in milliseconds</param>
        /// <param name="zag">The linear equation to follow while "zagging', this defines the angle and the speed at which the object will move independent of zig</param>
        /// <param name="timeToZag">This defines how long the object will use the zag equation for in milliseconds</param>
        public ZigZag(LinearLocationEquation zig, long timeToZig, LinearLocationEquation zag, long timeToZag)
        {
                 initialize(zig, timeToZig, zag, timeToZag);
        }
        
        /// <summary>
        ///     Create a new ZigZag equation that zigs and zags, this creates two independent linear equations to move along
        ///     and moves along each equation for the specified amount of time
        /// </summary>
        /// <param name="angleZig">The angle to zig at in radians, 0 is right and increasing values go clockwise</param>
        /// <param name="speedZig">The speed to zig at in pixels per millisecond</param>
        /// <param name="timeToZig">How long to zig for in milliseconds</param>
        /// <param name="angleZag">The angle to zag at in radians, 0 is right and increasing values go clockwise</param>
        /// <param name="speedZag">The speed the object will use while zagging in pixels per millisecond</param>
        /// <param name="timeToZag">How long to zag for in milliseconds</param>
        public ZigZag(double angleZig, double speedZig, long timeToZig, double angleZag, double speedZag, long timeToZag)
        {
            LinearLocationEquation zig = new LinearLocationEquation(angleZig, speedZig);
            LinearLocationEquation zag = new LinearLocationEquation(angleZag, speedZag);
            initialize(zig, timeToZig, zag, timeToZag);

        }

        private void initialize(LinearLocationEquation zig, long timeToZig, LinearLocationEquation zag, long timeToZag)
        {
            if (timeToZag < 0 || timeToZig < 0)
            {
                throw new ArgumentOutOfRangeException("timeToZig or timeToZag was less than 0");
            }
            
            this.zig = zig;
            this.timeToZig = timeToZig;
            
            this.zag = zag;
            this.timeToZag = timeToZag;
            lastPivotLoc = Vector2.Zero;
            wasZig = timeToZig > 0;
        }

        public Vector2 GetLocation(long ticksElapsed)
        {
            long modTick = getModTick(ticksElapsed);
            
            if (wasZig)
            {
                if (modTick < timeToZig)
                {
                    return lastPivotLoc + zig.GetLocation(modTick);
                }
                else
                {
                    lastPivotLoc += zig.GetLocation(timeToZig);
                    wasZig = false;
                    return lastPivotLoc + zag.GetLocation(modTick - timeToZig);
                }                
            }
            else
            {
                if (modTick < timeToZig)
                {
                    lastPivotLoc += zag.GetLocation(timeToZag);
                    wasZig = true;
                    return lastPivotLoc + zig.GetLocation(modTick);
                }
                else
                {
                    return lastPivotLoc + zag.GetLocation(modTick - timeToZig);
                }
            }
            
        }

        private long getModTick(long ticksElapsed)
        {
            long modTick = ticksElapsed % (timeToZag + timeToZig);

            return modTick;
        }
    }
}