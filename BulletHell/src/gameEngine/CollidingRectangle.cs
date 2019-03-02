using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine {
    public class CollidingRectangle : Hitbox {
        // public Rectangle rect {get{return _rect;}}
        public Rectangle rect;
        public int Width {get{return rect.Width;}}
        public int Height {get{return rect.Height;}}

        public CollidingRectangle(Vector2 parentLoc, Vector2 relLoc, int width, int height) : base(parentLoc, relLoc)
        {
            rect.Width = width;
            rect.Height = height;
            rect.X = (int)(absLoc.X);
            rect.Y = (int)(absLoc.Y);
        }
    }
}