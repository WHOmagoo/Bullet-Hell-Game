using System.Collections.Generic;
using BulletHellTests.GameEngine;

namespace BulletHellTests
{
    public class Collider
    {
        private static Collider collider = new Collider();
        List<GameObject> enemies;
        List<GameObject> friendlies;

        private Collider()
        {
            enemies = new List<GameObject>();
            friendlies = new List<GameObject>();
        }

        public static Collider getCollider()
        {
            return collider;
        }

        public void addFriendlyObject(GameObject go)
        {
            friendlies.Add(go);
        }

        public void addEnemyObject(GameObject go)
        {
            enemies.Add(go);
        }

    }

}