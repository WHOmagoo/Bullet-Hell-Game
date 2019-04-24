using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class ShotgunBulletFactory : BulletFactory
    {
        private ILocationEquation center;
        private ILocationEquation left;
        private ILocationEquation right;
        private double spread;

        public ShotgunBulletFactory(double spread, ILocationEquation general)
        {
            initialize(spread, general, general, general);
        }
        
        
        public ShotgunBulletFactory(double spread, ILocationEquation center, ILocationEquation left, ILocationEquation right)
        {
            initialize(spread, center, left, right);
        }

        private void initialize(double spread, ILocationEquation center, ILocationEquation left, ILocationEquation right)
        {
            this.center = center;
            this.left = left;
            this.right = right;
            this.spread = spread;
        }
        
        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset)
        {
            List<Bullet> result = new List<Bullet>();

            // Path pathLeftDiag = new BasicPath(fireShape, location, Math.PI / 8);
            // Path pathRightDiag = new BasicPath(fireShape, location, -Math.PI / 8);
            // Path pathDown = new BasicPath(fireShape, location, 0);
            Path pathLeftDiag = new BasicPath(left, location, spread + angleOffset);
            Path pathRightDiag = new BasicPath(right, location, -spread + angleOffset);
            Path pathDown = new BasicPath(center, location, angleOffset);

            Bullet b = new Bullet(1, pathLeftDiag, bulletTexture, team);
            prepareBullet(b, team);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);
            b = new Bullet(1, pathRightDiag, bulletTexture, team);
            prepareBullet(b, team);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);
            b = new Bullet(1, pathDown, bulletTexture, team);
            prepareBullet(b, team);
            BHGame.CollisionManager.addToTeam(b, team);
            result.Add(b);

            // Bullet b = new Bullet(damage, new LinearLocationEquation(direction, .1F), bulletTexture, location, team);
            // b.SetSize(20,30);
            // b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            // BHGame.CollisionManager.addToTeam(b, team);
            // result.Add(b);

            // b = new Bullet(damage, new LinearLocationEquation(weaponSpread + direction, .11F), this.bulletTexture, location, this.team);
            // b.SetSize(20,30);
            // b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            // BHGame.CollisionManager.addToTeam(b, team);
            // result.Add(b);

            // b = new Bullet(damage, new LinearLocationEquation(direction - weaponSpread, .11F), this.bulletTexture, location, this.team);
            // b.SetSize(20,30);
            // b.Hitbox = new CollidingRectangle(b.Location, new Vector2(0,0), 20, 30);
            // BHGame.CollisionManager.addToTeam(b, team);
            // result.Add(b);

            return result;
        }
    }
}