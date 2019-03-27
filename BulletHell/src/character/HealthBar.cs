using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class HealthBar : GameObject
    {
        //This class is in charch of displaying the red bar
        //we don't need an update funtion for this class because it will not need to change at all

        private Texture2D container;
        private Vector2 position;
        private int fullHealth;
        private int currentHealth;

        private Path path;

        public HealthBar(Texture2D healthBarTexture, Vector2 startLocation, Path p) : base(healthBarTexture, startLocation)
        {
            position = new Vector2(100, 100);
            //LoadContent(content,healthBarTexture);
            container = healthBarTexture;
            fullHealth = container.Width;
            currentHealth = fullHealth;

            path = p;
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
        public void isDead(object sender, EventArgs e)
        {
            BHGame.Canvas.RemoveFromDrawList(this);
        }

        public void SetPath(Path path)
        {
            this.path = path;
        }
        public void ResetPath()
        {
            this.path.Reset();
        }

        //private void LoadContent(ContentManager content,Texture2D healthBarTexture)
        //{
        //    container = healthBarTexture;
        //}

    }
}
