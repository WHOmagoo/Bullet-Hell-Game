using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Entity
    {
        private Texture2D texture;
        private Rectangle rect; //used for drawing on canvas
        private Vector2 location; //used to keep exact float position.
        private Canvas canvas;

        public Vector2 Location
        {
            get { return location; }
            set
            {
                location = value;
                rect.X = (int)value.X;
                rect.Y = (int)value.Y;
            }
        }
        public Rectangle Rect
        {
            get { return rect; }
        }
        //Will create entity with texture size equal to normal resolution.
        public Entity(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0)
        {
            this.canvas = canvas;
            this.texture = texture; //Size should be held within texture for drawing
            this.location = startLocation;
            if (width == 0 || height == 0)
            {
                this.rect = new Rectangle((int)location.X, (int)location.Y, texture.Bounds.Width, texture.Bounds.Height);
            }
            else
            {
                this.rect = new Rectangle((int)location.X, (int)location.Y, width, height);
            }
            canvas.AddToDrawList(this);
        }

        //Create entity with specific texture size given by rect.
        // public Entity(Canvas canvas, Texture2D texture, Rectangle rect)
        // {
        //     this.canvas = canvas;
        //     this.texture = texture; //Size should be held within texture for drawing
        //     this.rect = rect;
        //     this.location = new Vector2(rect.X, rect.Y);
        //     canvas.AddToDrawList(this);
        // }

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
            spriteBatch.Draw(texture, rect, Color.White);
        }
        public void Move(Vector2 dir)
        {
            Location = Location + dir;
        }
        public void SetSize(int width, int height)
        {
            rect.X = width;
            rect.Y = height;
        }
    }
}