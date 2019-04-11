using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
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

            isHealthbarVisible = true;
            _healthbar = null;
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
