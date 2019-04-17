using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gameEngine {
    //TODO: Implement
    public class MultiHitbox : Hitbox
    {
        public MultiHitbox(Vector2 parentLoc, Vector2 relLoc) : base(parentLoc, relLoc)
        {
        }

        public override Hitbox Copy()
        {
            throw new System.NotImplementedException();
        }

        public override void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            throw new System.NotImplementedException();
        }

    }
}