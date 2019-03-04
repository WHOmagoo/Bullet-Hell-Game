using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHellTests.GameEngine
{
    public class BasicGun : Gun
    {

        public BasicGun(int damage, ILocationEquation shape, Texture2D texture, long delay, bool friend) : base(damage, shape, texture, delay, friend)
        {
        }
        
        public override void shoot(Vector2 location){
            if(canShoot())
            {
                Bullet bullet =  makeBullet(location);
                Collider collider = Collider.getCollider();
                if(friendly)
                {
                    collider.addFriendlyObject(bullet);
                }
                else
                {
                    collider.addEnemyObject(bullet);
                }
                
                base.wasShot();
            }
            
        }

        private Bullet makeBullet(Vector2 location){
            return new Bullet(damage, this.fireShape, Canvas.getCanvas(), bulletTexture, location);
        }
    }

}