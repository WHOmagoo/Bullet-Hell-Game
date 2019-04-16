using System;
using BulletHell.bullet.factory;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    public class EnemyB : Enemy
    {
        public EnemyB(Texture2D texture, Path p) : base(texture, p, null)
        {
            InitializeEnemy();
        }
        private void InitializeEnemy()
        {
            healthPoints = 8;
            // this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, 1), 
            //     GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, TEAM.ENEMY);
            // this.gunEquipped = new BasicGun(1, new SpiralLocationEquation(1,1,.1f), 
            // this.gunEquipped = new BasicGun(1, new SinusoidalLocationEquation(90, 110, 200), 
            //     GraphicsLoader.getGraphicsLoader().getTexture("bullet"), 2000, TEAM.ENEMY, Math.PI/2);
//            this.gunEquipped = new BasicGun(1, new SinusoidalLocationEquation(90, 110, 200), 
//                GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2000, TEAM.ENEMY, Math.PI/2);
            this.gunEquipped = new Gun(1,GraphicsLoader.getGraphicsLoader().getTexture("bullet"), BulletFactoryFactory.make("singlesinusoidal"), TEAM.ENEMY);
        }
    }
}