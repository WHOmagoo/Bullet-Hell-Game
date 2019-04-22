using System;
using System.Collections.Generic;
using BulletHell;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
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
            Gun gun = gunFactory.makeGun(gunType);
            Enemy enemy = new Enemy(texture, path, health, gun);
            // enemy.SetSize(100,100);
            // enemy.Hitbox = new CollidingRectangle(enemy.Location, Vector2.Zero, 100, 100);
            enemy.Hitbox = HitboxRepo.getHitboxRepo().getHitbox(textureName);
            enemy.Scale(scale);
            enemy.healthbar = new HealthBar(enemy.Location, new Vector2(8, 0), 85, 90, enemy.Health);
            return enemy;
        }
    }
}