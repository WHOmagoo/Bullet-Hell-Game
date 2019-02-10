using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{

    public abstract class GameObject : Entity
    {
        public GameObject(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(canvas, texture, startLocation, width, height)
        {
        }

        // public GameObject(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        // {
        // }

        public virtual void Update() {}
    }
}