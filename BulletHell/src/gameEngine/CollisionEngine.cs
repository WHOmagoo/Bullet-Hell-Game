using System;
using System.Numerics;
using Microsoft.Xna.Framework;

// using System.Generic;
namespace BulletHell.GameEngine
{

    public static class CollisionEngine
    {

        public static bool isColliding(Hitbox h1, Hitbox h2)
        {
            if (h1 == null || h2 == null)
                return false;
            if (h1 is CollidingRectangle && h2 is CollidingRectangle)
                return isColliding((CollidingRectangle)h1, (CollidingRectangle)h2);
            if (h1 is CollidingCircle && h2 is CollidingCircle)
                return isColliding((CollidingCircle)h1, (CollidingCircle)h2);
            if (h1 is CollidingRectangle)
                return isColliding((CollidingRectangle)h1, (CollidingCircle)h2);
            else
                return isColliding((CollidingRectangle)h2, (CollidingCircle)h1);
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
            //Change to int for efficiency?
            System.Numerics.Vector2 rLoc = new System.Numerics.Vector2(r.absLoc.X + r.Width/2, r.absLoc.Y + r.Height/2);
            float circleDistanceX = Math.Abs(c.absLoc.X - rLoc.X);
            float circleDistanceY = Math.Abs(c.absLoc.Y - rLoc.Y);
            // float circleDistanceX = Math.Abs(c.absLoc.X - r.absLoc.X);
            // float circleDistanceY = Math.Abs(c.absLoc.Y - r.absLoc.Y);

            if (circleDistanceX > (r.Width / 2 + c.radius)) //Comparing float and int, will lose some precision
                return false;
            if (circleDistanceY > (r.Height / 2 + c.radius))
                return false;

            if (circleDistanceX <= (r.Width / 2))
                return true;
            if (circleDistanceY <= (r.Height / 2))
                return true;

            float cornerDistance_sq = (float)(Math.Pow((double)(circleDistanceX - r.Width / 2), 2) +
                Math.Pow(circleDistanceY - r.Height / 2, 2));
            return (cornerDistance_sq <= Math.Pow(c.radius, 2));
        }
    }
}