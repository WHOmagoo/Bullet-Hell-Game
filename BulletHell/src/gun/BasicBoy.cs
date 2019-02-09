using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class BaiscBoy : Gun 
    {

        public void shoot(Vector2 location){
            Clock clock = Clock.getClock();
            if(lastShotTick + tickShotDelay <= clock.getTime())
            {
                lastShotTick = clock.getTime();
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
            }
            
        }

        private Bullet makeBullet(Vector2 location){
            return new Bullet(damage, locationEquation, canvas, bulletTexture, location);
        }
    }

}