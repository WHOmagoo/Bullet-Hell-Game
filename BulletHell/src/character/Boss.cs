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

namespace BulletHell.character
{
    public class Boss : Enemy
    {

        public delegate void bossdeath();
        public event bossdeath BossDeathEvent;
        public Boss(Texture2D texture, Path p , int health, BulletFactory bulletFactory = null, float delay = 1):
            base(texture, p ,health, bulletFactory, delay){}

        public Boss(Enemy e, Vector2 startLocation) : base(e, startLocation){}

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