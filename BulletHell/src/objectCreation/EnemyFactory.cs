using System;
using System.Collections.Generic;
using BulletHell.bullet.factory;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using BulletHell.bullet.factory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.ObjectCreation
{
    public class EnemyFactory
    {
        private PathFactory pathFactory;
        private GunFactory gunFactory;
        public EnemyFactory(PathFactory pathFactory = null)
        {
            if (pathFactory == null)
                this.pathFactory = new PathFactory();
            else
                this.pathFactory = pathFactory;
            gunFactory = new GunFactory();
        }
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation,
                                List<PathData> pathData, string gun, float delay, double scale = 1)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, p, gun, delay, scale);
        }
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation,
                                 PathData pathData, string gun, float delay, double scale = 1)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, p, gun, delay, scale);
        }

        private Enemy makeEnemy(string textureName, int health, Path path, string gunType, float delay, double scale = 1)
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture(textureName);
            Enemy enemy;
            try
            {
                enemy = new Enemy(texture, path, health, BulletFactoryFactory.make(gunType), delay);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading gun " + gunType);
                enemy = new Enemy(texture, path, health);
            }
            Gun gun = gunFactory.makeGun(gunType);
            enemy.Hitbox = HitboxRepo.getHitboxRepo().getHitbox(textureName);
            enemy.Scale(scale);
            enemy.healthbar = new HealthBar(enemy.Location, new Vector2(8, 0), enemy.Rect.Width, 10, enemy.Health);

            // SpiralLocationEquation s = new SpiralLocationEquation(6, 40, 10);
            // BossGun g = new BossGun(path.InitialLocation+new Vector2(60,60), -2, new SingleBulletFactory(new LinearLocationEquation(Math.PI/2, .2F)), (float)(Math.PI/2), s, bossbullet_texture, TEAM.ENEMY);
            
            return enemy;
        }
    }
}