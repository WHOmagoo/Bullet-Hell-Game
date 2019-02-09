using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    class Player : Character
    {
        const string name2 = "download";
        const int startX = 125;
        const int startY = 245;

        internal Vector2 direction = Vector2.Zero;
        internal Vector2 speed = Vector2.Zero;

        internal Vector2 tempLoc = Vector2.Zero;

        int slow = 0;
        
        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public override void Update()
        {
            float timeE = Clock.getClock().getTimeSinceLastUpdate();
            float scale = 0.5;
            KeyboardState currState = Keyboard.GetState();
            UpdateMove(currState);
            if(slow) timeE *= scale;

            tempLoc += direction * speed * timeE / 50000;
            
            if(tempLoc.X > Window.ClientBounds.Width)
                Location.X = Window.ClientBounds.Width;
            if(tempLoc.Y > WIndow.ClientBounds.Height)
                Location.Y = Window.ClientBounds.Width;
            if(Location.X < 0) Location.X = 0;
            if(Location.Y < 0) Location.Y = 0;
            else
            {
                Location += direction * speed * timeE/ 50000;
            }
            
            
            base.Update();
        }
        
        
        //TODO refactor this so that this class subscribes to events from a controller class
        private void UpdateMove(KeyboardState currState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if(currState.IsKeyDown(Keys.LeftControl))
            {
                slow = 1;
            }
            else if(currState.IsKeyDown(Keys.RightControl))
            {
                slow = 0;
            }

            if(currState.IsKeyDown(Keys.Left))
            {
                InputControl.MoveLeft();
            }
            else if(currState.IsKeyDown(Keys.Right))
            {
                InputControl.MoveRight();
            }

            if(currState.IsKeyDown(Keys.Up))
            {
                InputControl.MoveUp();
            }
            else if(currState.IsKeyDown(Keys.Down))
            {
                InputControl.MoveDown();
            }
        }

        public Player(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(canvas, texture, startLocation)
        {
            InputControl.AssignPlayer(this);
        }

        public Player(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        {
            InputControl.AssignPlayer(this);
        }
    }
}
