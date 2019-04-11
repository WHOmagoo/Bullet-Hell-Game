using System.Collections.Generic;
using BulletHell.bullet;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.gun
{
    public class BasicGun : Gun
    {

        //TODO allow guns to spawn bullets at an offset from the location given to it in Shoot()s
        public BasicGun(int damage, ILocationEquation fireShape, Texture2D texture, long delay, TEAM team, 
                        double fireAngleOffset = 0) : base(damage, fireShape, texture, delay, team, fireAngleOffset)
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
            // Bullet b = new Bullet(damage, this.fireShape, bulletTexture, location, team);
            Path path = new Path(fireShape, location, fireAngleOffset);
            Bullet b = new Bullet(damage, path, bulletTexture, team);
            b.SetSize(20,30);
            b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            BHGame.CollisionManager.addToTeam(b, team);
            return b;
        }
    }

}