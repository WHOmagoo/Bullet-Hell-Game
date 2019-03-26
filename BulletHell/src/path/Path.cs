using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class Path
    {
        public Vector2 InitialLocation {get;}
        private ILocationEquation _locationEquation;
        private Vector2 Offset;
        private double AngleOffset;
        private long StartTime;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationEquation"> The ILocationEquation to use to update the location</param>
        /// <param name="initialLocation"> The starting location of the object</param>
        /// <param name="AngleOffset"> The input angle should be in radians and will go clockwise starting in the x direction</param>
        
        public Path(ILocationEquation locationEquation, Vector2 initialLocation, double AngleOffset)
        {
            _locationEquation = locationEquation;
            InitialLocation = initialLocation;
            Offset = initialLocation - locationEquation.GetLocation(0);
            this.AngleOffset = AngleOffset;
            StartTime = Clock.getClock().getTime();
        }

        public Path(Path path, Vector2 initialLocation, double AngleOffset)
        {
            _locationEquation = path._locationEquation;

            Offset = initialLocation - _locationEquation.GetLocation(0);

            this.AngleOffset = AngleOffset;

            this.StartTime = Clock.getClock().getTime();
        }

        public Vector2 UpdateLocation()
        {
            long curTime = Clock.getClock().getTime();

            Vector2 newLocation = _locationEquation.GetLocation(curTime - StartTime);
            newLocation = VectorRotation.RotateVector(AngleOffset, newLocation);
            
            return newLocation + Offset;
        }

        /*
            Resets the start time to current time
         */
        public void Reset()
        {
            this.StartTime = Clock.getClock().getTime();
        }
    }
}