using System;
using System.Collections.Generic;
using BulletHell.path;

namespace BulletHell.bullet.factory
{
    public class BulletFactoryFactory
    {
        
        /// <summary>
        /// the string key should be lowercase because the make function casts the name to lowercase when checking
        /// </summary>
        private static Dictionary<string, Func<BulletFactory>> nameToFunction;
        
        public static BulletFactory make(string gunName)
        {
            if(ReferenceEquals(null, nameToFunction))
            {
                initializeDictionary();
            }

            gunName = gunName.ToLower();
            
            if (nameToFunction.ContainsKey(gunName))
            {
                return nameToFunction[gunName]();
            }
            else
            {
                throw new Exception("Gun name not found");
            }            
        }

        
        //TODO possible allow dynamically populating the nameToFunction Dictionary to allow loading of custom preset guns
        private static void initializeDictionary()
        {
            nameToFunction = new Dictionary<string, Func<BulletFactory>>();
            nameToFunction.Add("shotgun", makeDefaultShotgun);
            nameToFunction.Add("basic", makeBasicGun);
            nameToFunction.Add("basic-fast", makeBasicFastGun);
            nameToFunction.Add("surround", makeSurroundBulletFactory);
            nameToFunction.Add("singlesinusoidal", makeSinusoidalBulletFactory);
            nameToFunction.Add("bossgun", makeBossGun);
            nameToFunction.Add("bossgun2", makeBossGun2);
            nameToFunction.Add("collidingcombinded", makeBulletWaveWithCollidingBulletWave);
            nameToFunction.Add("colliding", makeCollidingBullet);
//            nameToFunction.Add("zigzagshotgun", makeZigZagShotgun);
        }

        private static BulletFactory makeBossGun2()
        {
            // BulletFactory[] factories = new BulletFactory[]{makeDefaultShotgun(), makeSurroundBulletFactory(), makeBossSpiralFactory()};
            BulletFactory[] factories = new BulletFactory[]{makeBossSpiralFactory(), makeDefaultShotgun(), makeSurroundBulletFactory()};
            ChangingBulletFactoryData[] datas = new ChangingBulletFactoryData[factories.Length];

            int i = 0;
            foreach (var factory in factories)
            {
                datas[i] = new ChangingBulletFactoryData(factories[i], 1);
                i++;
            }

            datas[0].numberOfShots = 100;
            datas[0].shotDrops = 1;
            datas[1].numberOfShots = 3;
            datas[1].shotDrops = 100;
            datas[2].numberOfShots = 3;
            datas[2].shotDrops = 100;
            
            return new ChangingBulletFactory(datas);
        }
        private static BulletFactory makeBossGun()
        {
            BulletFactory secondColliding = new BulletWaveWithCollidingBullet(new SurroundBulletFactory(32, new SurroundBulletFactory(48, new SingleBulletFactory(new SinusoidalLocationEquation(90, 90, 200)))), new ShotgunBulletFactory(4 * Math.PI / 9, new LinearLocationEquation(0, .4F)));
            
            BulletFactory[] factories = 
                {makeDefaultShotgun(), makeBulletWaveWithCollidingBulletWave(),
                    makeDefaultShotgun(), makeSinusoidalBulletFactory(),
                    secondColliding
                };
            ChangingBulletFactoryData[] datas = new ChangingBulletFactoryData[factories.Length];

            int i = 0;
            foreach (var unused in factories)
            {
                datas[i] = new ChangingBulletFactoryData(factories[i], 2);
                i++;
            }

            datas[2].numberOfShots = 1;
//            datas[3].numberOfShots = 0;
            
            return new ChangingBulletFactory(datas);
        }

        private static BulletFactory makeBossSpiralFactory()
        {
            SpiralLocationEquation s = new SpiralLocationEquation(6, 40, 10);
            BulletFactory baseBf = new ShotgunBulletFactory(2*Math.PI/3, new LinearLocationEquation(Math.PI/2, .2F));
            MovingBulletFactory bf = new MovingBulletFactory(baseBf, s);
            return bf;
        }
        private static BulletFactory makeSinusoidalBulletFactory()
        {
            return new SingleBulletFactory(new SinusoidalLocationEquation(90, 110, 200)); 
        }

        private static BulletFactory makeSurroundBulletFactory()
        {
            return new SurroundBulletFactory(16, new  SingleBulletFactory(new SinusoidalLocationEquation(90, 110, 200)));
        }

        private static BulletFactory makeBasicGun()
        {
            return new SingleBulletFactory(new LinearLocationEquation(0, .2F));
        }
        private static BulletFactory makeBasicFastGun()
        {
            return new SingleBulletFactory(new LinearLocationEquation(0, .6F));
        }

        public static ShotgunBulletFactory makeDefaultShotgun()
        {
            return new ShotgunBulletFactory(Math.PI / 6, new LinearLocationEquation(0, .08F));
        }

//        public static ShotgunBulletFactory makeZigZagShotgun()
//        {
//            return new ShotgunBulletFactory(Math.PI / 6, new ZigZag(new LinearLocationEquation(0, .03F), 333, new LinearLocationEquation(-5 * Math.PI / 6,.03F), 333));
//        }

        public static BulletWaveWithCollidingBullet makeBulletWaveWithCollidingBulletWave()
        {
            BulletFactory bulletWave = new SurroundBulletFactory(66, new SingleBulletFactory(new LinearLocationEquation(0, .06F)));
            BulletFactory collidingBullets =
                new SurroundBulletFactory(8, new SingleBulletFactory(new LinearLocationEquation(0, .17F)));
            
            return new BulletWaveWithCollidingBullet(bulletWave, collidingBullets);
        }

        public static CollidingBulletFactory makeCollidingBullet()
        {
            return new CollidingBulletFactory(new SurroundBulletFactory(8, new SingleBulletFactory(new LinearLocationEquation(0, .14F))));
        }
    }
}