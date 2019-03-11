using System;
using System.Collections.Generic;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class BasicShotgun : Gun
    {

        private float weaponSpread;
        private float direction;
        
        public BasicShotgun(float direction, float spread, int damage, ILocationEquation shape, Texture2D texture, int delay, TEAM team) : base(damage, shape, texture, delay, team)
        {
            this.weaponSpread = spread;
            this.direction = direction;
        }

        public override void Shoot(Vector2 location)
        {
            if (canShoot())
            {
                var bullets = makeBullets(location);
                base.wasShot();
                OnShoot(bullets);
            }
        }
        
        private List<Bullet> makeBullets(Vector2 location){
            List<Bullet> result = new List<Bullet>();
            
            Bullet b = new Bullet(damage, new LinearLocationEquation(direction, .1F), bulletTexture, location, team);
            b.SetSize(20,30);
            b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);
            
            b = new Bullet(damage, new LinearLocationEquation(weaponSpread + direction, .11F), this.bulletTexture, location, this.team);
            b.SetSize(20,30);
            b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);

            b = new Bullet(damage, new LinearLocationEquation(direction - weaponSpread, .11F), this.bulletTexture, location, this.team);
            b.SetSize(20,30);
            b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);

            return result;
        }
    }

}