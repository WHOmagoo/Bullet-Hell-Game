using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine {
    //TODO: Implement
    public class MultiHitbox : Hitbox
    {
        public MultiHitbox(Vector2 parentLoc, Vector2 relLoc) : base(parentLoc, relLoc)
        {
        }

        public override void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            throw new System.NotImplementedException();
        }
    }
}