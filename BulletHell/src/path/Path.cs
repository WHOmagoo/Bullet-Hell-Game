using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class Path
    {
        private ILocationEquation _locationEquation;
        private Vector2 Offset;
        private double AngleOffset;
        private long StartTime;
        
        public Path(ILocationEquation locationEquation, Vector2 initialLocation, double AngleOffset)
        {
            _locationEquation = locationEquation;
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

            Vector2 newLocationBeforeAngleOffset = _locationEquation.GetLocation(curTime - StartTime) + Offset;

            float newLocationX =
                (float)(newLocationBeforeAngleOffset.X * Math.Cos(AngleOffset) -
                        newLocationBeforeAngleOffset.Y * Math.Sin(AngleOffset));
            
            float newLocationY =
                (float)(newLocationBeforeAngleOffset.X * Math.Sin(AngleOffset) +
                        newLocationBeforeAngleOffset.Y * Math.Cos(AngleOffset));
            
            return new Vector2(newLocationX, newLocationY);
        }
    }
}