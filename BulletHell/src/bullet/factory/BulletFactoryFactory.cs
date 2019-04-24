using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
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
            nameToFunction.Add("surround", makeSurroundBulletFactory);
            nameToFunction.Add("singlesinusoidal", makeSinusoidalBulletFactory);
            nameToFunction.Add("bossgun", makeBossGun);
        }

        private static BulletFactory makeBossGun()
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
            return new SurroundBulletFactory(16, (float) Math.PI / 2, (float) (Math.PI / 9),
                new  SingleBulletFactory(new SinusoidalLocationEquation(90, 110, 200)));
        }

        private static BulletFactory makeBasicGun()
        {
            return new SingleBulletFactory(new LinearLocationEquation(3.14, .2F));
        }

        public static ShotgunBulletFactory makeDefaultShotgun()
        {
            return new ShotgunBulletFactory(Math.PI / 6, new LinearLocationEquation(0, .08F));
        }
    }
}