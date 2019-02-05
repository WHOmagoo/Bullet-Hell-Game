﻿using System;
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
        /* 
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

        */

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
    }
}
