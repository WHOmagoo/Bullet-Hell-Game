namespace BulletHell.GameEngine
{
    public partial class Hitbox
    {

        public static class CollisionEngine
        {

            public static bool isColliding(Hitbox h1, Hitbox h2)
            {
                h1.GetType().IsClass(CollidingRectangle);
                return false;
            }

            public static bool isColliding(CollidingRectangle r1, CollidingRectangle r2)
            {
                return r1.rect.Intersects(r2.rect);
            }

        }
    }
}