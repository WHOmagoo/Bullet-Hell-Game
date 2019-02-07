using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        protected Path path;

        //need Gun class    TODO
        public Enemy(Canvas canvas, Texture2D texture, Vector2 startLocation, Path p/*,Gun gun,Path p*/) : base(canvas, texture, startLocation)
        {
            //gunEquipped = gun;
            path = p;
        }

        public Enemy(Canvas canvas, Texture2D texture, Rectangle rect, Path p/*, Gun gun,Path p*/) : base(canvas, texture, rect)
        {
            //gunEquipped = gun;
            path = p;
        }

        public override void Update()
        {
            this.Location = path.UpdateLocation();
            base.Update();
        }
    }
}
