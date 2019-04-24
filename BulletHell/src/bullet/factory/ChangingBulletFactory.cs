using System;
using System.Collections.Generic;
using BulletHell.gameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet.factory
{
    public class ChangingBulletFactory : BulletFactory
    {
        private ChangingBulletFactoryData[] patterns;
        private int curFiringPatternIndex;
        private int numberOfTimesShot;
        
        
        public ChangingBulletFactory(ChangingBulletFactoryData[] firingPatterns)
        {
            this.patterns = firingPatterns;
            curFiringPatternIndex = 0;
            numberOfTimesShot = 0;

            if (firingPatterns.Length == 0)
            {
                throw new ArgumentException("No firing patterns specified for ChangingBulletFactory");
            }
        }

        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            UpdateWeapon();

            numberOfTimesShot++;
            return patterns[curFiringPatternIndex].factory.makeBullets(location, bulletTexture, team, angleOffset);
        }

        private void UpdateWeapon()
        {
            if (patterns[curFiringPatternIndex].numberOfShots == 0)
            {
                return;
            }
            
            if (numberOfTimesShot == patterns[curFiringPatternIndex].numberOfShots)
            {
                numberOfTimesShot = 0;
                curFiringPatternIndex = (curFiringPatternIndex + 1) % patterns.Length;
                BulletFactory nextBF = patterns[curFiringPatternIndex].factory;
                if(nextBF is MovingBulletFactory)
                {
                    ((MovingBulletFactory)nextBF).ResetMovePath();
                }
            }
        }
    }

    public struct ChangingBulletFactoryData
    {
        public BulletFactory factory;
        public int numberOfShots;
        
        public ChangingBulletFactoryData(BulletFactory factory, int numberOfShots)
        {
            this.factory = factory;
            this.numberOfShots = numberOfShots;
        } 
    }
}