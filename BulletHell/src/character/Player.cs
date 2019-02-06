using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void LoadContent(ContentManager theContentManager)
        {
            Location = new Vector2(startX, startY);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState currState = Keyboard.GetState();
            UpdateMove(currState);

            base.Update(theGameTime, speed, direction);
        }
        
        private void UpdateMove(KeyboardState currState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if(currState.IsKeyDown(Keys.Left) == true)
            {
                InputControl.MoveLeft();
            }
            else if(currState.IsKeyDown(Keys.Right) == true)
            {
                InputControl.MoveRight();
            }

            if(currState.IsKeyDown(Keys.Up) == true)
            {
                InputControl.MoveUp();
            }
            else if(currState.IsKeyDown(Keys.Down) == true)
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
