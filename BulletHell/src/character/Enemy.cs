using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System;
using BulletHell.Pickups;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        private Path path;

        public EventHandler onDeath;

        protected Path Path
        {
            set
            {
                if (!ReferenceEquals(value, null))
                {
                    path = value;
                    Location = path.InitialLocation;
                }
            }
            
            get { return path; }
        }

        public Enemy(Texture2D texture, Path p , Gun gun = null) 
            : base(texture, p.InitialLocation)
        {
            healthPoints = 10;
            gunEquipped = gun;
            path = p;
        }

        public Enemy(Enemy e) : base(e.texture, e.Path.InitialLocation)
        {
            gunEquipped = e.gunEquipped;
            healthPoints = e.healthPoints;
            path = e.Path;
        }

        public override void Update()
        {
            this.Location = path.UpdateLocation();
            base.Update();
        }

        private void CreateDeathEvent()
        {
            onDeath?.Invoke(this, new EventArgs());
        }


        protected override void Die()
        {
            //CreateDeathEvent();
            //CreatePickupEvent();
            BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
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

    }
}
