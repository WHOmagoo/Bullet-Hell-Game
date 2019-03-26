using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class Player : Character
    {
        const string name2 = "download";
        const int startX = 125;
        const int startY = 245;

        internal Vector2 direction = Vector2.Zero;
        internal Vector2 speed = Vector2.Zero;
        private Vector2 respawnLocation;

        public event EventHandler OnHit;

        Canvas canvas;
        int slow = 0;

        public event EventHandler DeathEvent;

        public Player(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(texture, startLocation)
        {
            invulnerable = true;
            this.respawnLocation = startLocation;
            this.canvas = canvas;
            InputControl.AssignPlayer(this);
            gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), 
                GraphicsLoader.getGraphicsLoader().getBulletTexture(), 500, TEAM.FRIENDLY);
            
            // gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, true);

            //TODO make this be a parameter
            healthPoints = 3;      //player lives
        }

        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public int Lives
        {
            get { return healthPoints; }
        }

        public override void Update()
        {
            base.Update();
            float timeE = Clock.getClock().getTimeSinceLastUpdate();
            float scale = 0.5F;
            KeyboardState currState = Keyboard.GetState();
            UpdateMove();
            if (slow == 1) timeE *= scale;

            if (Rect.X + Rect.Width > canvas.GetBounds().Width)
                Location = new Vector2(canvas.GetBounds().Width - Rect.Width - 1, Location.Y);
            if (Rect.Y + Rect.Height > canvas.GetBounds().Height)
                Location = new Vector2(Location.X, canvas.GetBounds().Height - Rect.Height - 1);

            if (Location.X < 0) Location = new Vector2(1, Location.Y);
            if (Location.Y < 0) Location = new Vector2(Location.X, 1);
            else
            {
                Location += direction * speed * timeE / 50000;
            }
            base.Update();
        }

        
        private void UpdateMove()
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            Controller controller = new Controller();
            controller.OnLeft += goLeft;
            controller.OnRight += goRight;
            controller.OnUp += goUp;
            controller.OnDown += goDown;
            controller.OnUpLeft += goUpLeft;
            controller.OnUpRight += goUpRight;
            controller.OnDownLeft += goDownLeft;
            controller.OnDownRight += goDownRight;
            controller.OnShoot += Shoot;
            controller.OnSlow += goSlow;
            controller.OnFast += goFast;

            controller.Update();
            
        }

        private void goLeft(object sender, EventArgs e)
        {
            InputControl.MoveLeft();
        }

        private void goRight(object sender, EventArgs e)
        {
            InputControl.MoveRight();
        }

        private void goUp(object sender, EventArgs e)
        {
            InputControl.MoveUp();
        }

        private void goDown(object sender, EventArgs e)
        {
            InputControl.MoveDown();
        }

        private void goUpLeft(object sender, EventArgs e)
        {
            // InputControl.MoveUpLeft();
        }

        private void goUpRight(object sender, EventArgs e)
        {
            // InputControl.MoveUpRight();
        }

        private void goDownLeft(object sender, EventArgs e)
        {
            // InputControl.MoveDownLeft();
        }

        private void goDownRight(object sender, EventArgs e)
        {
            // InputControl.MoveDownRight();
        }

        private void Shoot(object sender, EventArgs e)
        {
            gunEquipped.Shoot(Location);
        }

        private void goSlow(object sender, EventArgs e)
        {
            slow = 1;
        }

        private void goFast(object sender, EventArgs e)
        {
            slow = 0;
        }

        public void updateHealth()
        {
            healthPoints -= 1;
            if (healthPoints == 0)
            {
                //respawn
                CreateDeathEvent();
            }

        }
        public override void onCollision(GameObject hitby)
        {
            if (!invulnerable)
            {
                OnHit(this, EventArgs.Empty);
                updateHealth();
                startInvulnerability();
                base.onCollision(hitby);
                Location = respawnLocation;
                Console.WriteLine("Player Health: " + healthPoints);
            }
        }

        private Thread t;
        private volatile bool invulnerable = false;
        private void startInvulnerability()
        {
            if (invulnerable)
            {
                t.Abort();
            }
            
            t = new Thread(invulnerableRunner);
            t.Start();
        }

        private void invulnerableRunner()
        {
            Console.WriteLine("Invulnerable");
            invulnerable = true;
            Thread.Sleep(5000);
            invulnerable = false;
            Console.WriteLine("Can now take damage again");
        }

        protected override void TakeDamage(int damage)
        {
            updateHealth();
        }

        private void CreateDeathEvent()
        {
            DeathEvent?.Invoke(this, new EventArgs());
        }

        protected override void Die()
        {
            BHGame.CollisionManager.removeFromTeam(this, TEAM.FRIENDLY);
        }
    }
}
