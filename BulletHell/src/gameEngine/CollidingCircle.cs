using BulletHell.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gameEngine {
    public class CollidingCircle : Hitbox {
        public float radius;

        public CollidingCircle(Vector2 parentLoc, Vector2 relLoc, float radius) : base(parentLoc, relLoc)
        {
            this.radius = radius;
        }
        public override Hitbox Copy()
        {
            return new CollidingCircle(_parentLoc, _relLoc, radius);
        }


        public override void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            DrawingTool.DrawCircle(spriteBatch, this._absLoc, radius, Color.Red, 1, 16);
        }

        public override void Scale(double scale)
        {
            _relLoc = new Vector2((int)(_relLoc.X * scale), (int)(_relLoc.Y * scale));
            radius = (int)(radius * scale);
        }
    }
}