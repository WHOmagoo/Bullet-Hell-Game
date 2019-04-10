using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{

    public class GraphicsLoader
    {
        private GraphicsDevice graphicsDevice;
        private static GraphicsLoader graphicsLoader;
        private Hashtable textureTable;

        private GraphicsLoader(GraphicsDevice g)
        {
            this.graphicsDevice = g;
            this.LoadTextures(g);
        }


        //TODO make check if graphics loader is already initialized
        public static GraphicsLoader getGraphicsLoader(GraphicsDevice g)
        {
            if (graphicsLoader == null)
                graphicsLoader = new GraphicsLoader(g);
            return graphicsLoader;
        }

        public Texture2D getTexture(string textureName)
        {

            return null;
        }

        public bool addTexture(string name, string path)
        {
            Texture2D t = Texture2D.FromStream(graphicsDevice, new FileStream(path, FileMode.Open));
            try
            {
                textureTable.Add(name, t);
                return true;
            }
            catch
            {
                //Already in table
                return false;
            }
        }

        private void LoadTextures(GraphicsDevice graphicsDevice)
        {
            addTexture("player", "Content/sprites/shuttle.png");
            addTexture("enemyA", "Content/sprites/enemyA.png");
            addTexture("enemyB", "Content/sprites/white-ghost.png");
            addTexture("enemyC", "Content/sprites/octopus.png");
            addTexture("midBoss", "Content/sprites/midboss.png");
            addTexture("finalBoss", "Content/sprites/finalboss.png");
            addTexture("healthBar", "Content/sprites/healthBar.png");
            addTexture("lifeBar", "Content/sprites/lifeBar.png");
            addTexture("bullet", "Content/sprites/bullet.png");
        }
    }
}