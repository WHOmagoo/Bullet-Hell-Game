using System;
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

        Canvas canvas;
        int slow = 0;

        public Player(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(texture, startLocation)
        {
            this.canvas = canvas;
            InputControl.AssignPlayer(this);
            gunEquipped = new BasicGun(1, new LinearLocationEquation(-Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, true);
            
        }

        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public override void Update() 
        {
            base.Update();
            float timeE = Clock.getClock().getTimeSinceLastUpdate();
            float scale = 0.5F;
            KeyboardState currState = Keyboard.GetState();
            UpdateMove(currState);
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


        //TODO: refactor this so that this class subscribes to events from a controller class
        private void UpdateMove(KeyboardState currState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if (currState.IsKeyDown(Keys.LeftControl))
            {
                slow = 1;
            }
            else if (currState.IsKeyDown(Keys.RightControl))
            {
                slow = 0;
            }

            if (currState.IsKeyDown(Keys.Left))
            {
                InputControl.MoveLeft();
            }
            else if (currState.IsKeyDown(Keys.Right))
            {
                InputControl.MoveRight();
            }

            if (currState.IsKeyDown(Keys.Up))
            {
                InputControl.MoveUp();
            }
            else if (currState.IsKeyDown(Keys.Down))
            {
                InputControl.MoveDown();
            }

            if (currState.IsKeyDown(Keys.Space))
            {
                gunEquipped.Shoot(Location);
            }
        }
    }
}
