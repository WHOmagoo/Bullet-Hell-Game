using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class Path
    {
        private ILocationEquation _locationEquation;
        private Vector2 Offset;
        private double AngleOffset;
        private int StartTime;
        
        public Path(ILocationEquation locationEquation, Vector2 initialLocation, double AngleOffset)
        {
            _locationEquation = locationEquation;
            Offset = initialLocation - locationEquation.GetLocation(0);
            this.AngleOffset = AngleOffset;
            StartTime = Clock.getClock().getTime();
        }

        public Vector2 UpdateLocation()
        {
            int curTime = Clock.getClock().getTime();

            Vector2 newLocationBeforeAngleOffset = Equation.updateLocation(curTime - StartTime) + Offset;

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