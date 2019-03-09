using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    class Controller
    {
        public event EventHandler OnLeft;
        public event EventHandler OnRight;
        public event EventHandler OnUp;
        public event EventHandler OnDown;
        public event EventHandler OnShoot;
        public event EventHandler OnSlow;
        public event EventHandler OnFast;
        public event EventHandler OnUpLeft;
        public event EventHandler OnUpRight;
        public event EventHandler OnDownLeft;
        public event EventHandler OnDownRight;

        public Controller()
        {
        }

        public void Update()
        {
            KeyboardState currState = Keyboard.GetState();

            if (currState.IsKeyDown(Keys.LeftControl))
            {
                OnSlow(this, EventArgs.Empty);
            }
            else if (currState.IsKeyDown(Keys.RightControl))
            {
                OnFast(this, EventArgs.Empty);
            }

            if (currState.IsKeyDown(Keys.Left) && OnLeft != null)
            {
                OnLeft(this, EventArgs.Empty);
            }
            else if (currState.IsKeyDown(Keys.Right) && OnRight != null)
            {
                OnRight(this, EventArgs.Empty);
            }

            if (currState.IsKeyDown(Keys.Up) && OnUp != null)
            {
                OnUp(this, EventArgs.Empty);
            }
            else if (currState.IsKeyDown(Keys.Down) && OnDown != null)
            {
                OnDown(this, EventArgs.Empty);
            }

            if (currState.IsKeyDown(Keys.W) && OnUpLeft != null)
            {
                OnUpLeft(this, EventArgs.Empty);
            }
            else if (currState.IsKeyDown(Keys.E) && OnUpRight != null)
            {
                OnUpRight(this, EventArgs.Empty);
            }

            if (currState.IsKeyDown(Keys.S) && OnDownLeft != null)
            {
                OnDownLeft(this, EventArgs.Empty);
            }
            else if (currState.IsKeyDown(Keys.D) && OnDownRight != null)
            {
                OnDownRight(this, EventArgs.Empty);
            }

            if (currState.IsKeyDown(Keys.Space) && OnShoot != null)
            {
                OnShoot(this, EventArgs.Empty);
            }
        }
    }
}
