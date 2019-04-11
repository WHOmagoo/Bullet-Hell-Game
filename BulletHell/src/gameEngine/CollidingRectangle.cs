using BulletHell.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gameEngine
{
    public class CollidingRectangle : Hitbox
    {
        // public Rectangle rect {get{return _rect;}}
        public Rectangle rect;
        public int Width { get { return rect.Width; } }
        public int Height { get { return rect.Height; } }
        public override Vector2 parentLoc //Location of the GameObject's locations
        {
            set { base.parentLoc = value; 
                rect.X = (int)base._absLoc.X; rect.Y = (int)base._absLoc.Y; } //update rect on parentLoc being updated
            get { return _parentLoc; }
        }

        public CollidingRectangle(Vector2 parentLoc, Vector2 relLoc, int width, int height) : base(parentLoc, relLoc)
        {
            rect.Width = width;
            rect.Height = height;
            rect.X = (int)(absLoc.X);
            rect.Y = (int)(absLoc.Y);
        }

        public override void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            DrawingTool.DrawRectangle(spriteBatch, rect, color, lineWidth);
        }
    }
}