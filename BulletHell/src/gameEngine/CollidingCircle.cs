using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine {
    public class CollidingCircle : Hitbox {
        public float radius;

        public CollidingCircle(Vector2 parentLoc, Vector2 relLoc, float radius) : base(parentLoc, relLoc)
        {
            this.radius = radius;
        }

        public override void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            DrawingTool.DrawCircle(spriteBatch, this._absLoc, radius, Color.Red, 1, 16);
        }
    }
}