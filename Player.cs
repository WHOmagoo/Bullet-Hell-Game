using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace test4
{
    public class Player
    {
        public Rectangle rect;
        public Texture2D texture;
        public Player(GraphicsDevice graphicsDevice) {
            FileStream fileStream = new FileStream("Content/sprites/shuttle.png", FileMode.Open);
            texture = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();
            rect = new Rectangle(20,20,200,240);
        }

        public void update() {
            
        }
    }
}