using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;



namespace BulletHell.GameEngine
{
    // const int N_OF_TEAMS = 3;
    public enum TEAM { FRIENDLY = 0, ENEMY, UNASSIGNED}
    public class CollisionManager
    {
        List<GameObject>[] teams;
        const int N_OF_TEAMS = 3; //= Enum.GetNames(TEAM).Length; //FIXME: make const and dynamically get nteams

        public CollisionManager()
        {
            teams = new List<GameObject>[N_OF_TEAMS];
            for (int i = 0; i < N_OF_TEAMS; i++) {
                teams[i] = new List<GameObject>();
            }
        }

        public void addToTeam(GameObject g, TEAM t)
        {
            if (g.Hitbox == null)
                throw new NullReferenceException("Gameobject doesn't have a hitbox for collision manager");
            teams[(int)t].Add(g);
        }

        public void removeFromTeam(GameObject g, TEAM t)
        {
            teams[(int)t].Remove(g);
        }

        /**
            Goes through different teams and runs collision checks. If collided
            notifies the corresponding GameObjects
         */
        public void runCollisions()
        {
            for (int i = 0; i < N_OF_TEAMS; i++)
            {
                for (int j = i + 1; j < N_OF_TEAMS; j++)
                {
                    runCollisionsBetweenTeams(teams[i], teams[j]);
                }
            }
        }

        private Boolean isColliding(GameObject g1, GameObject g2)
        {
            return CollisionEngine.isColliding(g1.Hitbox, g2.Hitbox);
        }

        private void runCollisionsBetweenTeams(List<GameObject> t1, List<GameObject> t2)
        {
            foreach (GameObject g1 in t1)
            {
                foreach (GameObject g2 in t2)
                {
                    if(isColliding(g1,g2))
                        onCollide(g1,g2);
                }
            }
        }

        private void onCollide(GameObject g1, GameObject g2)
        {
            g1.onCollision(g2);
            g2.onCollision(g1);
        }
    }
}