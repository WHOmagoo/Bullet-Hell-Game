using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace test4
{
    public class Enemy : Character
    {
        //private Path path;    need path class     TODO

        //need Gun class    TODO
        public Enemy(Canvas canvas, Texture2D texture, Vector2 startLocation/*,Gun gun,Path p*/) : base(canvas, texture, startLocation)
        {
            //gunEquipped = gun;
            //path = p;
        }

        public Enemy(Canvas canvas, Texture2D texture, Rectangle rect/*, Gun gun,Path p*/) : base(canvas, texture, rect)
        {
            //gunEquipped = gun;
            //path = p;
        }
    }
}
