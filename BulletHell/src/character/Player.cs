using System;
using System.Threading;
using System.Timers;
using BulletHell.controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using BulletHell.Pickups;

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
        public int slow = 0;

        public event EventHandler DeathEvent;

        public Player(Canvas canvas, Texture2D texture, Vector2 startLocation, Controller controller) : base(texture, startLocation)
        {
            // invulnerable = true;
            this.respawnLocation = startLocation;
            this.canvas = canvas;
            InputControl.AssignPlayer(this);
            gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), 
                GraphicsLoader.getGraphicsLoader().getTexture("bullet"), 500, TEAM.FRIENDLY);
            
            // gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, true);

            //TODO make this be a parameter
            healthPoints = 3;      //player lives
            subscribeToController(controller);
        }

        private void subscribeToController(Controller controller)
        {
            controller.OnLeft += goLeft;
            controller.OnRight += goRight;
            controller.OnUp += goUp;
            controller.OnShoot += Shoot;
            controller.OnSlow += goSlow;
            controller.OnFast += goFast;
            controller.OnDown += goDown;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public int Lives
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }

        public override void Update()
        {
            base.Update();
            float timeE = Clock.getClock().getTimeSinceLastUpdate();
            float scale = 0.5F;
            float scale2 = 2.0F;

            //timeE = timeE * scale * slow;
            if (slow == 1) timeE *= scale;
            if (slow == 2) timeE *= scale2;

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
            
            direction = Vector2.Zero;
            speed = Vector2.Zero;

            base.Update();
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
            Console.WriteLine("Player health: " + healthPoints);
            if (healthPoints == 0)
            {
                //respawn
                CreateDeathEvent();
            }

        }
        public override void onCollision(GameObject hitby)
        {
            // Console.WriteLine("hitby: " + hitby);
            if (!invulnerable && !(hitby is Pickup))
            {
                OnHit(this, EventArgs.Empty);
                updateHealth();
                startInvulnerability();
                base.onCollision(hitby);
                Location = respawnLocation;
                // Console.WriteLine("Player Health: " + healthPoints);
            }
        }

        private Thread t;
        public volatile bool invulnerable = false;
        public bool drawSp = true;
        public System.Timers.Timer myT = new System.Timers.Timer();
   
        private void myEv(object source, ElapsedEventArgs e)
        {
            drawSp = !drawSp;
        }


        private void startInvulnerability()
        {
            if (invulnerable)
            {
                t.Abort();
            }
            
            invulnerable = true;
            t = new Thread(invulnerableRunner);
            t.Start();
        }
        
        //create new timer on every collision to make player "blink"
        private void newTimer()
        {
            myT = new System.Timers.Timer();
            myT.Elapsed += new ElapsedEventHandler(myEv);
            myT.AutoReset = true;
            myT.Interval = 100; //can change for "blink" interval
            myT.Start();
        }

        private void invulnerableRunner()
        {
            Console.WriteLine("Invulnerable");
            // invulnerable = true;
            newTimer();
            Thread.Sleep(2000);
            invulnerable = false;
            myT.Dispose();
            drawSp = true;
            Console.WriteLine("Can now take damage again");
        }

        protected override void TakeDamage(int damage)
        {
            // updateHealth();
            //do nothing already handled in oncollision FIXME: Clean this system up
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
