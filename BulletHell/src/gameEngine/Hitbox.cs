using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gameEngine
{

    public abstract class Hitbox
    {

        protected Vector2 _absLoc;
        protected Vector2 _parentLoc;
        protected Vector2 _relLoc;
        public Vector2 absLoc { get { return _relLoc + _parentLoc; } }
        public virtual Vector2 parentLoc //Location of the GameObject's locations
        {
            set { _parentLoc = value; _absLoc = _relLoc + _parentLoc; }
            get { return _parentLoc; }
        } 
        public Vector2 relLoc; //Location relative to the GameObject
        public Hitbox(Vector2 parentLoc, Vector2 relLoc)
        {
            this._relLoc = relLoc;
            this.parentLoc = parentLoc;
        }
        public abstract void DrawHitbox(SpriteBatch spriteBatch, Color color, int lineWidth);
        // public abstract bool isColliding(Hitbox h);
    }
}