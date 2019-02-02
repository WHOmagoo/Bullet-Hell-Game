

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace test4
{

    public abstract class GameObject : Entity
    {
        public GameObject(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(canvas, texture, startLocation)
        {
        }

        public GameObject(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        {
        }

        public virtual void Update() {}
    }
}