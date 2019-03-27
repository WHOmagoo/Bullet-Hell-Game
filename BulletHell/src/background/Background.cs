using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 vector1;
        public Vector2 vector2;

        private int fullHeight;

        public Background(Texture2D bgTexture, Vector2 startLocation)
        {
            position = new Vector2(0, 0);
            texture = bgTexture;
            vector1 = startLocation;
            vector2 = startLocation;
            vector2.Y += texture.Height;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector2, Color.White);
            spriteBatch.Draw(texture, vector1, Color.White);
        }

        public void  Update(int x, int y)
        {
            
            
            if(vector1.Y + texture.Height <= 0)
            {
                vector1.Y = vector2.Y + texture.Width;
            }
            if(vector2.Y + texture.Height <= 0)
            {
                vector2.Y = vector1.Y + texture.Height;
            }
            if(x >= 0)
            {
                vector1.X += x;
                vector2.X += x;

            }
            if(x < 0)
            {
                vector1.X -= x - (x * 2);
                vector2.X -= x - (x * 2);
            }

            if(y >= 0)
            {
                vector1.Y += y;
                vector2.Y += y;
            }
            if(y < 0)
            {
                vector1.Y -= y - (y * 2);
                vector2.Y -= y - (y * 2);
            }
        }
    }
    
}
