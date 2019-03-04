using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{

    public abstract class GameObject : Entity
    {
        private Hitbox hitbox;

        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }

        public GameObject(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(canvas, texture, startLocation, width, height)
        {
            hitbox = null;
        }

        // public GameObject(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        // {
        // }

        public virtual void Update() {}
        public virtual void onCollision(GameObject hitby) {}
    }
}