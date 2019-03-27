using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class LifeBar : GameObject
    {
        //This class is in charge of displaying the green bar whose width decreases are the
        //player's health declines

        private Texture2D lifeBar;
        private Vector2 position;
        private int fullHealth;
        private int fullHeight;
        private int currentHealth;

        private Path path;

        public LifeBar(Texture2D lifeBarTexture, Vector2 startLocation, Path p,int maxhealth,int maxheight) : base(lifeBarTexture, startLocation)
        {
            position = new Vector2(100, 100);
            //LoadContent(content, lifeBarTexture);
            lifeBar = lifeBarTexture;
            fullHealth = maxhealth;   //width of HealthBar
            fullHeight = maxheight;    //height of HealthBar
            currentHealth = fullHealth;

            path = p;
        }
        //private void LoadContent(ContentManager content, Texture2D lifeBarTexture)
        //{
        //    lifeBar = lifeBarTexture;
        //}
        public void Update(object sender, EventArgs e)
        {
            currentHealth -= (fullHealth/10);
            SetSize(currentHealth, fullHeight);
        }
        public void isDead(object sender, EventArgs e)
        {
            BHGame.Canvas.RemoveFromDrawList(this);
        }
        protected Path Path
        {
            set
            {
                if (!ReferenceEquals(value, null))
                {
                    path = value;
                }
            }

            get { return path; }
        }
        public override void Update()
        {
            this.Location = path.UpdateLocation();
            base.Update();
        }

        public void SetPath(Path path)
        {
            this.path = path;
        }
        public void ResetPath()
        {
            this.path.Reset();
        }

    }
}
