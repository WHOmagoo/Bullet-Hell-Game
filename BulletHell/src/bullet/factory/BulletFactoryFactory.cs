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
            nameToFunction.Add("surround", makeSurroundBulletFactory);
            nameToFunction.Add("singlesinusoidal", makeSinusoidalBulletFactory);
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
            return new SingleBulletFactory(new LinearLocationEquation(0, .2F));
        }

        public static ShotgunBulletFactory makeDefaultShotgun()
        {
            return new ShotgunBulletFactory(Math.PI / 6, new LinearLocationEquation(0, .08F));
        }
    }
}