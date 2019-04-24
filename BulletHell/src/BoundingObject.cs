using BulletHell.gameEngine;
using BulletHell.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{
    public class BoundingObject : GameObject
    {
        private Canvas c;
        public BoundingObject(Texture2D texture, Vector2 startLocation, Canvas c, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
            isSpriteVisible = false;
            this.c = c;
        }

        public override void onCollision(GameObject hitby)
        {
            c.RemoveFromDrawList(hitby);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}