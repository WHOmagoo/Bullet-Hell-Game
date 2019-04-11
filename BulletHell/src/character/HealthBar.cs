using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    class HealthBar : GameObject
    {
        //This class is in charch of displaying the red bar
        //we don't need an update funtion for this class because it will not need to change at all

        private Texture2D container;
        private Vector2 position;
        private int fullHealth;
        private int currentHealth;

        public HealthBar(Texture2D healthBarTexture, Vector2 startLocation) : base(healthBarTexture, startLocation)
        {
            position = new Vector2(100, 100);
            //LoadContent(content,healthBarTexture);
            container = healthBarTexture;
            fullHealth = container.Width;
            currentHealth = fullHealth;
        }

        //private void LoadContent(ContentManager content,Texture2D healthBarTexture)
        //{
        //    container = healthBarTexture;
        //}

    }
}
