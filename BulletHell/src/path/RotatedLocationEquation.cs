using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public class RotatedLocationEquation : ILocationEquation
    {
        private ILocationEquation equation;
        private double angle;

        public RotatedLocationEquation(ILocationEquation locationEquation, double angle)
        {
            this.equation = locationEquation;
            this.angle = angle;
        }
        
        public Vector2 GetLocation(long ticksElapsed)
        {
            Vector2 newLoc = equation.GetLocation(ticksElapsed);

            return VectorRotation.RotateVector(angle, newLoc);
        }
    }
}