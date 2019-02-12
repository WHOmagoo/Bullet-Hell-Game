using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Enemy : Character
    {
        protected Path path;
        protected Gun gunEquipped;

        public Enemy(Canvas canvas, Texture2D texture, Vector2 startLocation, Path p , Gun gun) 
            : base(canvas, texture, startLocation)
        {
            gunEquipped = gun;
            path = p;
        }

        public override void Update(long curTime)
        {
            this.Location = path.UpdateLocation(curTime);
            base.Update(curTime);
        }

        public void Shoot(long curTime)
        {
            gunEquipped.shoot(Location, curTime);
        }
    }
}
