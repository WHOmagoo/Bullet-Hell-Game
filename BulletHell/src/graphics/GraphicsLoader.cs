using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{

    public class GraphicsLoader
    {
        private GraphicsDevice graphicsDevice;

        private Texture2D bulletTexture;
        private Texture2D playerTexture;
        private Texture2D enemyATexture;
        private Texture2D enemyBTexture;
        private Texture2D midBossTexture;
        private Texture2D finalBossTexture;
        private Texture2D healthBarTexture;
        private Texture2D lifeBarTexture;
        private Texture2D enemyCTexture;

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
        private void LoadTextures(GraphicsDevice graphicsDevice)
        {
            playerTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/shuttle.png", FileMode.Open));

            enemyATexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            // enemyBTexture = Texture2D.FromStream(graphicsDevice,
            //     new FileStream("Content/sprites/enemyB.png", FileMode.Open));
            enemyBTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/white-ghost.png", FileMode.Open));

            midBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));

            finalBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/finalboss.png", FileMode.Open));

            healthBarTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/healthBar.png", FileMode.Open));

            lifeBarTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/lifeBar.png", FileMode.Open));
            bulletTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/bullet.png", FileMode.Open));
            enemyCTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/octopus.png", FileMode.Open));
        }
    }
}