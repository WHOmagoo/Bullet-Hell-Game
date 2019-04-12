using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.path;

namespace BulletHell.bullet.factory
{
    public class BulletFactoryFactory
    {
        private static Dictionary<string, Func<BulletFactory>> nameToFunction;
        
        public static BulletFactory make(int fireRate = 1)
        {
            if(ReferenceEquals(null, nameToFunction))
            {
                initializeDictionary();
            }
            
            return new ShotgunBulletFactory(1, new LinearLocationEquation(6, 1));
        }

        private static void initializeDictionary()
        {
            nameToFunction = new Dictionary<string, Func<BulletFactory>>();
            nameToFunction.Add("shotgun", makeDefaultShotgun);
            nameToFunction.Add("basic", makeBasicGun);
            nameToFunction.Add("surround", makeSurroundBulletFactory);
            
        }

        private static BulletFactory makeSurroundBulletFactory()
        {
            return new SurroundBulletFactory(16, (float) Math.PI / 2, (float) (Math.PI / 9), new  SingleBulletFactory(new SinusoidalLocationEquation(90, 110, 200)));
        }

        private static BulletFactory makeBasicGun()
        {
            return new SingleBulletFactory(new LinearLocationEquation(0, .08F));
        }

        public static ShotgunBulletFactory makeDefaultShotgun()
        {
            return new ShotgunBulletFactory(Math.PI / 6, new LinearLocationEquation(0, .08F));
        }
    }
}