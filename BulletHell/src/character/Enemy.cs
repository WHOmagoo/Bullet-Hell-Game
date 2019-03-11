using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        private Path path;

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

        public Enemy(Texture2D texture, Vector2 startLocation, Path p , Gun gun) 
            : base(texture, startLocation)
        {
            healthPoints = 10;
            gunEquipped = gun;
            path = p;
        }

        public override void Update()
        {
            this.Location = path.UpdateLocation();
            base.Update();
        }

        protected override void Die()
        {
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
