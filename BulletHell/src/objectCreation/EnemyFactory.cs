using System;
using System.Collections.Generic;
using BulletHell;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using BulletHell.bullet.factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.ObjectCreation {
    public class EnemyFactory
    {
        private PathFactory pathFactory;
        private GunFactory gunFactory;
        public EnemyFactory(PathFactory pathFactory = null) 
        {
            if(pathFactory == null)
                this.pathFactory = new PathFactory();
            else
                this.pathFactory = pathFactory;
            gunFactory = new GunFactory();
        }
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation, 
                                List<PathData> pathData, string gun, double scale = 1)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, p, gun, scale);
        }
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation,
                                 PathData pathData, string gun, double scale = 1)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, p, gun, scale);
        }

        private Enemy makeEnemy(string textureName, int health, Path path, string gunType, double scale)
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture(textureName);
            Texture2D texture2 = GraphicsLoader.getGraphicsLoader().getTexture("bullet");

            //Gun gun = gunFactory.makeGun(gunType);
            //BulletFactoryFactory f = new makeSurroundBulletFactory();
            SpiralLocationEquation s = new SpiralLocationEquation(6, 40, 10);
            BossGun g = new BossGun(path.InitialLocation, -2, new SingleBulletFactory(new LinearLocationEquation(Math.PI/2, .2F)), (float)(Math.PI/2), s, texture2, TEAM.ENEMY);
            //Gun g = new Gun(1, texture, BulletFactoryFactory.make("shotgun"), TEAM.ENEMY);
            Enemy enemy = new Enemy(texture, path, health, g);
            //enemy.gunEquipped.GunShotHandler += BHGame.Canvas.OnGunShot();
            enemy.Hitbox = HitboxRepo.getHitboxRepo().getHitbox(textureName);
            enemy.Scale(scale);
            enemy.healthbar = new HealthBar(enemy.Location, new Vector2(8, 0), enemy.Rect.Width,
                                            10, enemy.Health);
            
            return enemy;
        }
    }
}