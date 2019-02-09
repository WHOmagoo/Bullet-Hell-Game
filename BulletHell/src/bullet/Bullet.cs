using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Bullet : GameObject
    {
        int damage;
        Path pathToFollow;

        public Bullet(int damage, ILocationEquation locationEquation, Canvas canvas, Texture2D texture, Vector2 startLocation) : base(canvas, texture, startLocation)
        {
            this.damage = damage;
            this.pathToFollow = new Path(locationEquation, startLocation, 0);
        }

        public override void Update()
        {
            super.Move(pathToFollow.UpdateLocation());
        }

    }
}