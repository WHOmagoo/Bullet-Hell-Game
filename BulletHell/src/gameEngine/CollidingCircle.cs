using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine {
    public class CollidingCircle : Hitbox {
        public float radius;

        public CollidingCircle(Vector2 parentLoc, Vector2 relLoc, float radius) : base(parentLoc, relLoc)
        {
            this.radius = radius;
        }
    }
}