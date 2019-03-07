using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class Bullet : GameObject
    {
        int damage;
        Path pathToFollow;

        //TODO decide if we should take in ILocationEquation and make a path or accept a Path object within bullet
        public Bullet(int damage, ILocationEquation locationEquation, Texture2D texture, Vector2 startLocation) : base(texture, startLocation)
        {
            this.damage = damage;
            this.pathToFollow = new Path(locationEquation, startLocation, 0);
        }

        public override void Update()
        {
            Location = pathToFollow.UpdateLocation();
        }

    }
}