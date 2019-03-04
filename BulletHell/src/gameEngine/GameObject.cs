using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System.IO;
using System;

namespace BulletHell.GameEngine
{

    public class GameObject : Entity
    {
        private int counter; //FIXME: Get rid of this later
        private Hitbox hitbox;
        public bool isHitboxVisible;
        public override Vector2 Location {
            get { return base.Location;}
            set { base.Location = value; 
                if (hitbox != null) hitbox.parentLoc = value;} //Update hitbox location on move
        }

        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }

        public GameObject(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(canvas, texture, startLocation, width, height)
        {
            isHitboxVisible = true; //Probably default to false after testing
            hitbox = null;
        }

        // public GameObject(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        // {
        // }

        public virtual void Update() {}
        public virtual void onCollision(GameObject hitby) {
            Console.WriteLine(counter + "I got hit by: " + hitby.ToString());
            counter++;
        }
        public override void Draw(SpriteBatch spriteBatch) 
        {
            base.Draw(spriteBatch);
            if (hitbox != null && isHitboxVisible)
                hitbox.DrawHitbox(spriteBatch, Color.Red, 1);
        }
    }
}