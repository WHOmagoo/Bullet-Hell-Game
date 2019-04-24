using System;
using BulletHell.bullet;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using BulletHell.Pickups;
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
            
            healthPoints = e.healthPoints;
            path = e.Path.Copy();
            path.ResetAt(startLocation);
            this.healthbar = e.healthbar;
            if (ReferenceEquals(null, bf))
            {
                gunEquipped = new Gun(e.gunEquipped);
            }
            else
            {
                this.gunEquipped = new Gun(delay, GraphicsLoader.getGraphicsLoader().getTexture("enemy-bullet"), bf, TEAM.ENEMY);
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
            Random r = new Random();
            int ran = r.Next(0, 30);
            if (ran == 1 || ran == 2) { makeDamagePickup(); }
            if (ran == 3 || ran == 4) { makeFastPickup(); }
            if (ran == 5 || ran == 6) { makeLifePickup(); }

            BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
            BHGame.Canvas.RemoveFromDrawList(this);
        }

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

        public LifePickup makeLifePickup()
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture("lifepickup");
            LifePickup dp = new LifePickup(texture, new Vector2(this.Location.X, this.Location.Y), 80, 80);
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
