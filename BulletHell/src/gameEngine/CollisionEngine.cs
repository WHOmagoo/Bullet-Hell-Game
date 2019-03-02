using System.Math;
namespace BulletHell.GameEngine
{
    public partial class Hitbox
    {

        public static class CollisionEngine
        {

            public static bool isColliding(Hitbox h1, Hitbox h2)
            {
                //TODO: if elses to call corresponding function.
                // if(h1.GetType().IsClass(CollidingRectangle))
                //     return isColliding(h1,h2);
                return false;
            }

            public static bool isColliding(CollidingRectangle r1, CollidingRectangle r2)
            {
                return r1.rect.Intersects(r2.rect);
            }
            public static bool isColliding(CollidingCircle c1, CollidingCircle c2)
            {
                //Maybe compare lengthSquared for efficiency?
                //Make integer calculations for efficiency?
                return ((c1.absLoc - c2.absLoc).Length() < (c1.radius + c2.radius));
            }
            public static bool isColliding(CollidingRectangle r, CollidingCircle c)
            {
               int circleDistance.x = abs() 

            }

        }
    }
}