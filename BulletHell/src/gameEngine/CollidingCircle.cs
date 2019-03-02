using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine {
    public class CollidingCircle : Hitbox {
        public float radius;

        public CollidingCircle(Vector2 absLoc, Vector2 relLoc, float radius) : base(absLoc, relLoc)
        {
            this.radius = radius;
        }
    }
}