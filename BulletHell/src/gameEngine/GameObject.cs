using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{

    public abstract class GameObject : Entity
    {
        private Hitbox hitbox;
        public bool isHitboxVisible;

        public Hitbox Hitbox { get => hitbox; set => hitbox = value; }

        public GameObject(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(canvas, texture, startLocation, width, height)
        {
            isHitboxVisible = true; //Probably default to false after testing
            hitbox = null;
        }

        // public GameObject(Canvas canvas, Texture2D texture, Rectangle rect) : base(canvas, texture, rect)
        // {
        // }

        public virtual void Update() {
            if (hitbox != null)
                hitbox.parentLoc = Location;
        }
        public virtual void onCollision(GameObject hitby) {}
        public override void Draw(SpriteBatch spriteBatch) 
        {
            base.Draw(spriteBatch);
            if (hitbox != null && isHitboxVisible)
                hitbox.DrawHitbox(spriteBatch, Color.Red, 1);
        }
    }
}