using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class BasicGun : Gun
    {

        public BasicGun(int damage, ILocationEquation shape, Texture2D texture, long delay, bool friend) : base(damage, shape, texture, delay, friend)
        {
        }
        
        public override void Shoot(Vector2 location){
            if(canShoot())
            {
                Bullet bullet =  makeBullet(location);
                
                List<Bullet> bullets = new List<Bullet>();
                bullets.Add(bullet);
                
                OnShoot(bullets);
                
                base.wasShot();
            }
            
        }

        private Bullet makeBullet(Vector2 location){
            Bullet b = new Bullet(damage, this.fireShape, bulletTexture, location);
            b.SetSize(20,30);
            b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            BHGame.CollisionManager.addToTeam(b, TEAM.ENEMY);
            return b;
        }
    }

}