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
        private int untilNextShot;
        
        
        public ChangingBulletFactory(ChangingBulletFactoryData[] firingPatterns)
        {
            this.patterns = firingPatterns;
            curFiringPatternIndex = 0;
            untilNextShot = patterns[curFiringPatternIndex].shotDrops;
            numberOfTimesShot = 0;

            if (firingPatterns.Length == 0)
            {
                throw new ArgumentException("No firing patterns specified for ChangingBulletFactory");
            }
        }

        public override List<Bullet> makeBullets(Vector2 location, Texture2D bulletTexture, TEAM team, double angleOffset = 0)
        {
            UpdateWeapon();

            untilNextShot--;
            if(untilNextShot <= 0)
            {
                untilNextShot = patterns[curFiringPatternIndex].shotDrops;
                numberOfTimesShot++;
                return patterns[curFiringPatternIndex].factory.makeBullets(location, bulletTexture, team, angleOffset);
            }
            return null;
        }

        private void UpdateWeapon()
        {
            if (patterns[curFiringPatternIndex].numberOfShots == 0)
            {
                return;
            }
            
            if (numberOfTimesShot >= patterns[curFiringPatternIndex].numberOfShots)
            {
                numberOfTimesShot = 0;
                curFiringPatternIndex = (curFiringPatternIndex + 1) % patterns.Length;
                untilNextShot = patterns[curFiringPatternIndex].shotDrops;
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
        public int shotDrops;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="numberOfShots"></param>
        /// <param name="shotDrops"></param> Shoots every xth shot. 10 will shoot every 10th shot. 1 will shoot every shot
        public ChangingBulletFactoryData(BulletFactory factory, int numberOfShots, int shotDrops = 1)
        {
            this.factory = factory;
            this.numberOfShots = numberOfShots;
            this.shotDrops = shotDrops;
        } 
    }
}