using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{

    public class GraphicsLoader
    {
        private GraphicsDevice graphicsDevice;

        private Texture2D bulletTexture;

        private static GraphicsLoader graphicsLoader;

        private GraphicsLoader(GraphicsDevice g)
        {
            this.graphicsDevice = g;
        }


        //TODO make check if graphics loader is already initialized
        public static bool makeGraphicsLoader(GraphicsDevice g)
        {
            graphicsLoader = new GraphicsLoader(g);

            return true;
        }

        public static GraphicsLoader getGraphicsLoader()
        {
            return graphicsLoader;
        }

        public void setGraphicsTexture(FileStream stream)
        {
            bulletTexture = Texture2D.FromStream(graphicsDevice, stream);
        }

        public Texture2D getBulletTexture()
        {
            return bulletTexture;
        }
    }
}
