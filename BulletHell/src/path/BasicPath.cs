using System;
using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public class BasicPath : Path
    {
        private ILocationEquation _locationEquation;
        private double _speedRatio;
        private Vector2 Offset;
        private double AngleOffset;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationEquation"> The ILocationEquation to use to update the location</param>
        /// <param name="initialLocation"> The starting location of the object</param>
        /// <param name="AngleOffset"> The input angle should be in radians and will go clockwise starting in the x direction</param>
        
        public BasicPath(ILocationEquation locationEquation, Vector2 initialLocation, double AngleOffset, double speedRatio = 1)
        {
            _locationEquation = locationEquation;
            _speedRatio = speedRatio;
            _initialLocation = initialLocation;
            Offset = initialLocation - locationEquation.GetLocation(0);
            this.AngleOffset = AngleOffset;
            StartTime = Clock.getClock().getTime();
        }

        public BasicPath(BasicPath path, Vector2 initialLocation, double AngleOffset, double speedRatio = 1)
        {
            _locationEquation = path._locationEquation;
            _speedRatio = speedRatio;

            Offset = initialLocation - _locationEquation.GetLocation(0);

            this.AngleOffset = AngleOffset;

            this.StartTime = Clock.getClock().getTime();
        }

        public override Path Copy()
        {
            return new BasicPath(this, InitialLocation, AngleOffset, _speedRatio);
        }

        public override Vector2 UpdateLocation()
        {
            long curTime = (long)(Clock.getClock().getTime() * _speedRatio);

            Vector2 newLocation = _locationEquation.GetLocation(curTime - StartTime);
            newLocation = VectorRotation.RotateVector(AngleOffset, newLocation);
            
            return newLocation + Offset;
        }
    }
}