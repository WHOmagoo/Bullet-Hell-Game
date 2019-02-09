using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class StayStill : ILocationEquation
    {
        private static StayStill reference = new StayStill();

        private StayStill()
        {
        }

        public static StayStill getStayStill()
        {
            return reference;
        }

        public Vector2 GetLocation(long ticksElapsed)
        {
            return Vector2.Zero;
        }
    }
}