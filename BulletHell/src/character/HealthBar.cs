using BulletHell.gameEngine;
using BulletHell.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    public class HealthBar
    {
        public Rectangle _bar;
        public Rectangle _green_bar;
        protected Vector2 _absLoc;
        protected Vector2 _parentLoc;
        protected Vector2 _relLoc;
        public Vector2 absLoc { get { return _relLoc + _parentLoc; } }
        public int Width { get { return _bar.Width; } }
        public int Height { get { return _bar.Height; } }
        private int _one_life;
        public Vector2 parentLoc
        {
            set
            {
                _parentLoc = value;
                _bar.X = (int)absLoc.X; _bar.Y = (int)absLoc.Y;
                _green_bar.X = (int)absLoc.X; _green_bar.Y = (int)absLoc.Y;
            }
            get { return _parentLoc; }
        }
        public Vector2 relLoc;
        public HealthBar(Vector2 parentLoc, Vector2 relLocation, int width, int height, int health)
        {
            this._relLoc = relLocation;
            this.parentLoc = parentLoc;
            _bar.Width = width;
            _bar.Height = height;
            _bar.X = (int)(absLoc.X);
            _bar.Y = (int)(absLoc.Y);

            _green_bar.Width = width;
            _green_bar.Height = height;
            _green_bar.X = (int)(absLoc.X);
            _green_bar.Y = (int)(absLoc.Y);

            _one_life = width / health;
        }

        public HealthBar Copy(){
            return new HealthBar(parentLoc, _relLoc, _bar.Width, _bar.Height, _one_life * _bar.Width);
        }
        public void DrawHealthBar(SpriteBatch spriteBatch, Color color, int lineWidth)
        {
            DrawingTool.DrawRectangle(spriteBatch, _bar, color, lineWidth);
            DrawingTool.DrawRectangle(spriteBatch, _green_bar, Color.LightGreen, lineWidth);
        }
        public void UpdateHealth(int damage)
        {
            _green_bar.Width += damage * _one_life;
            //can add lives or subtract lives depending if damage is positive or negative
        }

    }
}
