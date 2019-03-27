using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        private Path path;
        public event EventHandler OnHit;
        public event EventHandler DeathEvent;

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

        public Enemy(Texture2D texture, Vector2 startLocation, Path p, Gun gun)
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
        public void updateHealth()
        {
            healthPoints -= 1;
            if (healthPoints == 0)
            {
                Die();
            }

        }
        public override void onCollision(GameObject hitby)
        {
            if (hitby is Bullet)
            {
                Bullet b = hitby as Bullet;
                TakeDamage(b.Damage);
                OnHit(this, EventArgs.Empty);
            }
        }
        protected override void TakeDamage(int damage)
        {
            updateHealth();
        }

        protected override void Die()
        {
            BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
            BHGame.Canvas.RemoveFromDrawList(this);
            DeathEvent(this, EventArgs.Empty);
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
