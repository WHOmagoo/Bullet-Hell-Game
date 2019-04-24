using BulletHell.gameEngine;
using BulletHell.gun;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.Graphics;
using BulletHell.bullet;
using System;
using BulletHell.Pickups;
using BulletHell.bullet.factory;
using BulletHell.graphics;

namespace BulletHell.character
{
    public class Boss : Enemy
    {

        public delegate void bossdeath();
        public event bossdeath BossDeathEvent;
        public Boss(Texture2D texture, Path p , int health, BulletFactory bulletFactory = null, float delay = 1):
            base(texture, p ,health, bulletFactory, delay){ }

        public Boss(Enemy e, Vector2 startLocation) : base(e, startLocation)
        {
            // SpiralLocationEquation s = new SpiralLocationEquation(6, 40, 10);
            // // BossGun g = new BossGun(Location+new Vector2(60,60), -2, 
            // //     new SingleBulletFactory(new LinearLocationEquation(Math.PI/2, .2F)), 
            // //     (float)(Math.PI/2), s, 
            // //     GraphicsLoader.getGraphicsLoader().getTexture("enemy-bullet"), TEAM.ENEMY);
            // // BulletFactory baseBf = new SingleBulletFactory(new LinearLocationEquation(Math.PI/2, .2F));
            // BulletFactory baseBf = new ShotgunBulletFactory(2*Math.PI/3, new LinearLocationEquation(Math.PI/2, .2F));
            // MovingBulletFactory bf = new MovingBulletFactory(baseBf, s);
            // Gun g = new Gun(.01f, GraphicsLoader.getGraphicsLoader().getTexture("enemy-bullet"), bf, TEAM.ENEMY, 0);
            // gunEquipped = g;
        }

        protected override void Die()
        {
            if(BossDeathEvent != null){
                BossDeathEvent();
            }
            Console.WriteLine("dead");
            BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
            BHGame.Canvas.RemoveFromDrawList(this);
        }


    }


}