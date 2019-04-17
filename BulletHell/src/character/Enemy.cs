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

        public Enemy(Texture2D texture, Path p , Gun gun = null) 
            : base(texture, p.InitialLocation)
        {
            //healthPoints = 10;
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
            Random r = new Random();
            int ran = r.Next(0, 6);
            if(ran == 1 || ran == 2) { makeDamagePickup(); } 
            if(ran == 3 || ran == 4) { makeFastPickup(); }
            //makepickup();
            BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
            BHGame.Canvas.RemoveFromDrawList(this);
        }

        //permanently increase damage
        public DamagePickup makeDamagePickup()
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture("dmgPickup");
            DamagePickup dp = new DamagePickup(texture, new Vector2(this.Location.X, this.Location.Y), 80, 80);
            dp.Hitbox = new CollidingCircle(dp.Location, new Vector2(dp.Rect.Width / 2, dp.Rect.Height / 2), 15);
            BHGame.CollisionManager.addToTeam(dp, TEAM.ENEMY);
            BHGame.Canvas.AddToDrawList(dp);
            return dp;
        }

        //increase speed temporarily
        public FastPickup makeFastPickup()
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture("fastPickup");
            FastPickup dp = new FastPickup(texture, new Vector2(this.Location.X, this.Location.Y), 80, 80);
            dp.Hitbox = new CollidingCircle(dp.Location, new Vector2(dp.Rect.Width / 2, dp.Rect.Height / 2), 15);
            BHGame.CollisionManager.addToTeam(dp, TEAM.ENEMY);
            BHGame.Canvas.AddToDrawList(dp);
            return dp;
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
