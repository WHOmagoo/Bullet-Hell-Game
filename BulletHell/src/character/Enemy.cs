using System;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.Graphics;

namespace BulletHell.character
{
    public class Enemy : Character
    {
        private Path path;
        protected HealthBar _healthbar;
        public bool isHealthbarVisible;

        public override Vector2 Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                if (_healthbar != null) _healthbar.parentLoc = value;
                if (hitbox != null) hitbox.parentLoc = value;

            } //Update hitbox and healthbar location on move
        }

        public HealthBar healthbar { get => _healthbar; set => _healthbar = value; }
        public int Health { get => healthPoints;}

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

        public Enemy(Texture2D texture, Path p , int health, BulletFactory bulletFactory = null, float delay = 1) 
            : base(texture, p.InitialLocation)
        {
            healthPoints = health;
            
            this.gunEquipped = new Gun(delay, GraphicsLoader.getGraphicsLoader().getBulletTexture(), bulletFactory, TEAM.ENEMY, -Math.PI / 2);
            path = p;

            isHealthbarVisible = true;
            _healthbar = null;
        }

        public Enemy(Enemy e, Vector2 startLocation, BulletFactory bf = null, int delay = 1) : base(e.texture, startLocation)
        {
            this.SetSize(e.Rect.Width, e.Rect.Height);
            hitbox = e.hitbox.Copy();
            gunEquipped = e.gunEquipped;
            healthPoints = e.healthPoints;
            path = e.Path.Copy();
            path.ResetAt(startLocation);
            this.healthbar = e.healthbar;
            if (ReferenceEquals(null, bf))
            {
                this.gunEquipped = e.gunEquipped;
            }
            else
            {
                this.gunEquipped = new Gun(delay, GraphicsLoader.getGraphicsLoader().getBulletTexture(), bf, TEAM.ENEMY);
            }
        }

        public override void onCollision(GameObject hitby)
        {
            if (hitby is Bullet)
            {
                isHealthbarVisible = true;
                Bullet b = hitby as Bullet;
                TakeDamage(b.Damage);
                healthbar.UpdateHealth(-1 * b.Damage);  //have to multiply by -1 for it to be negative
            }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (isHitboxVisible)
                hitbox?.DrawHitbox(spriteBatch, Color.Red, 1);
            if (isHealthbarVisible)
                _healthbar?.DrawHealthBar(spriteBatch, Color.Red, 6);
        }

    }
}
