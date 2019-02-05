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
    class Player : Sprite
    {
        const string name2 = "download";
        const int startX = 125;
        const int startY = 245;
        const int charSpeed = 160;
        const int moveU = -1;
        const int moveD = 1;
        const int moveL = -1;
        const int moveR = 1;

        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;

        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(startX, startY);
            base.LoadContent(theContentManager, name2);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState currState = Keyboard.GetState();
            UpdateMove(currState);

            base.Update(theGameTime, speed, direction);
        }

        private void MoveLeft()
        {
            speed.X = charSpeed;
            direction.X = moveL;
        }

        private void MoveRight()
        {
            speed.X = charSpeed;
            direction.X = moveR;
        }

        private void MoveDown()
        {
            speed.Y = charSpeed;
            direction.Y = moveD;
        }

        private void MoveUp()
        {
            speed.Y = charSpeed;
            direction.Y = moveU;
        }

        private void UpdateMove(KeyboardState currState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if(currState.IsKeyDown(Keys.Left) == true)
            {
                MoveLeft();
            }
            else if(currState.IsKeyDown(Keys.Right) == true)
            {
                MoveRight();
            }

            if(currState.IsKeyDown(Keys.Up) == true)
            {
                MoveUp();
            }
            else if(currState.IsKeyDown(Keys.Down) == true)
            {
                MoveDown();
            }
        }
    }
}
