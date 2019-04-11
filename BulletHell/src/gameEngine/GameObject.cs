using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System;

namespace BulletHell.GameEngine
{

    public class GameObject : Entity
    {
        private int counter; //FIXME: Get rid of this later
        protected Hitbox hitbox;
        public bool isHitboxVisible;
        public bool isSpriteVisible;
        public override Vector2 Location {
            get { return base.Location;}
            set { base.Location = value; 
                if (hitbox != null) hitbox.parentLoc = value;} //Update hitbox location on move
        }

        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }

        public GameObject(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(texture, startLocation, width, height)
        {
            isHitboxVisible = true; //Probably default to false after testing
            isSpriteVisible = true;
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
            if (isSpriteVisible)
                base.Draw(spriteBatch);
            //if (isHitboxVisible)
            //    hitbox?.DrawHitbox(spriteBatch, Color.Red, 1);
        }
    }
}