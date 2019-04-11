using System;
using System.Collections.Generic;
using BulletHell;
using BulletHell.GameEngine;
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
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation, List<PathData> pathData, string gun)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, startingLocation, p, gun);
        }
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation, PathData pathData, string gun)
        {
            Path p = pathFactory.makePath(startingLocation, pathData);
            return makeEnemy(textureName, health, startingLocation, p, gun);
        }

        private Enemy makeEnemy(string textureName, int health, Path path, string gunType)
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture(textureName);
            Gun gun = gunFactory.makeGun(gunType);
            Enemy enemy = new Enemy(texture, path, gun);
            return enemy;
        }
    }
}