namespace BulletHell
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

        public addFriendlyObject(GameObject go)
        {
            friendlies.Add(go);
        }

        public addEnemyObject(GameObject go)
        {
            enemies.Add(go);
        }

    }

}