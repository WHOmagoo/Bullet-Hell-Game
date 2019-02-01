using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace test4
{
    public class Entity
    {
        private Texture2D texture;
        private Vector2 location;
        private Canvas canvas;

        public Vector2 Location{
            get {return location;}
            set {location = value;}
        }
        //Used to get width and height of texture in pixels and location.
        public Rectangle Bounds { 
            get {return texture.Bounds;}
        }


        public Entity(Canvas canvas, Texture2D texture, Vector2 startLocation)
        {
            this.canvas = canvas;
            this.texture = texture; //Size should be held within texture for drawing
            this.location = startLocation;
            canvas.AddToDrawList(this);
        }

        ~Entity()
        {
            canvas.RemoveFromDrawList(this);
        }
        /*
            This function should only be called by Canvas.
            Draws the Entity on the screen with its texture and location.
        */
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
        }
        public void Update()
        {

        }
        public void Move(Vector2 dir)
        {

        }
    }
}