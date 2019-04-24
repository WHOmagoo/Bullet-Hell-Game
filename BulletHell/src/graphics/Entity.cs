using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.graphics
{
    public class Entity
    {
        protected internal Texture2D texture;
        private Rectangle rect; //used for drawing on canvas
        private Vector2 location; //used to keep exact float position.

        public virtual Vector2 Location
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
        /**  Will create entity with texture size equal to normal resolution. If no width or height passed in
         *  then it will set to the normal resolution, else will go to specified resolution.
         * */

        public Entity(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0)
        {
            this.location = startLocation;

            if (texture != null)
            {

                this.texture = texture; //Size should be held within texture for drawing
                if (width == 0 || height == 0)
                {
                    this.rect = new Rectangle((int) location.X, (int) location.Y, texture.Bounds.Width,
                        texture.Bounds.Height);
                }
                else
                {
                    this.rect = new Rectangle((int) location.X, (int) location.Y, width, height);
                }
            }
        }
        
        /*
            This function should only be called by Canvas.
            Draws the Entity on the screen with its texture and location.
        */
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
        public void Move(Vector2 dir)
        {
            Location = Location + dir;
        }
        public void SetSize(int width, int height)
        {
            rect.Width = width;
            rect.Height = height;
            
        }
    }
}