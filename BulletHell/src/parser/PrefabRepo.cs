using System;
using System.Collections.Generic;
using BulletHell.character;

namespace BulletHell
{
    public class PrefabRepo
    {
        private static PrefabRepo prefabRepo;
        private Dictionary<string, Enemy> EnemyPrefabs;

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

        public void emptyEnemyPrefabs()
        {
            EnemyPrefabs.Clear();
        }
    }
}
