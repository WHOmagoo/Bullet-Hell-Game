using System;
using System.Collections.Generic;
using BulletHell.bullet.factory;
using BulletHell.character;

namespace BulletHell
{
    public class PrefabRepo
    {
        private static PrefabRepo prefabRepo;
        private Dictionary<string, Enemy> EnemyPrefabs;
        private Dictionary<string, BulletFactory> bulletFactoryPrefab;

        private PrefabRepo()
        {
            EnemyPrefabs = new Dictionary<string, Enemy>();
        }

        public static PrefabRepo getPrefabRepo()
        {
            if (prefabRepo == null)
                prefabRepo = new PrefabRepo();
            return prefabRepo;
        }

        public Enemy getEnemyPrefab(string name)
        {
            Enemy e;
            try
            {
                e = EnemyPrefabs[name];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid name of Enemy");
            }
            return e;
        }

        public BulletFactory getBulletFactoryPrefab(string name)
        {
            BulletFactory b;

            try
            {
                return bulletFactoryPrefab[name];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid name of BulletFactory");
            }
        }

        public void registerEnemyPrefab(string name, Enemy prefab)
        {
            try
            {
                EnemyPrefabs.Add(name, prefab);
            }
            catch(ArgumentException)
            {
                throw new ArgumentException("Name " + name + " already in the enemy prefab dictionary.");
            }
        }

        public void registerBulletFactoryPrefab(string name, BulletFactory factory)
        {
            try
            {
                bulletFactoryPrefab.Add(name, factory);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Name " + name + " already registered to BulletFactoryPrefab");
            }
        }

        public void emptyEnemyPrefabs()
        {
            EnemyPrefabs.Clear();
        }
    }
}
