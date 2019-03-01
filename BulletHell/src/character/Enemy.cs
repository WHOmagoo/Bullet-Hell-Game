using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        protected Path path;

        public Enemy(Texture2D texture, Vector2 startLocation, Path p , Gun gun) 
            : base(texture, startLocation)
        {
            gunEquipped = gun;
            path = p;
        }

        public override void Update()
        {
            this.Location = path.UpdateLocation();
            base.Update();
        }
    }
}
