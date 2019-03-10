using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    class LifeBar : GameObject
    {
        //This class is in charge of displaying the green bar whose width decreases are the
        //player's health declines

        private Texture2D lifeBar;
        private Vector2 position;
        private int fullHealth;
        private int fullHeight;
        private int currentHealth;

        public LifeBar(Texture2D lifeBarTexture, Vector2 startLocation) : base(lifeBarTexture, startLocation)
        {
            position = new Vector2(100, 100);
            //LoadContent(content, lifeBarTexture);
            lifeBar = lifeBarTexture;
            fullHealth = 390;   //width of HealthBar
            fullHeight = 40;    //height of HealthBar
            currentHealth =fullHealth;
        }
        //private void LoadContent(ContentManager content, Texture2D lifeBarTexture)
        //{
        //    lifeBar = lifeBarTexture;
        //}
        public void Update(object sender, EventArgs e)
        {
            currentHealth -= 39;
            SetSize(currentHealth, fullHeight);
        }
        
    }
}
