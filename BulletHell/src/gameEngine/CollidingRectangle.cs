using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine {
    public class CollidingRectangle : Hitbox {
        // public Rectangle rect {get{return _rect;}}
        public Rectangle rect;

        public CollidingRectangle(Vector2 absLoc, Vector2 relLoc, int width, int height) : base(absLoc, relLoc)
        {
            rect.Width = width;
            rect.Height = height;
            rect.X = (int)(absLoc.X + relLoc.X);
            rect.Y = (int)(absLoc.Y + relLoc.Y);
        }
    }
}