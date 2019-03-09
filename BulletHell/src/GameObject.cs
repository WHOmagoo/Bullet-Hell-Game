using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{

    public abstract class GameObject : Entity
    {
        public GameObject(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(texture, startLocation, width, height)
        {
        }

        // public GameObject(Texture2D texture, Rectangle rect) : base(texture, rect)
        // {
        // }

        public virtual void Update() {}
    }
}