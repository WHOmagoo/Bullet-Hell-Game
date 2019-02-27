using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;



namespace BulletHell.GameEngine
{
    // const int N_OF_TEAMS = 3;
    enum TEAM { Friendly = 0, Enemy, Unassigned };
    public class CollisionManager
    {
        List<GameObject>[] teams;
        const int N_OF_TEAMS = Enum.GetNames(TEAM).Length;

        public CollisionManager()
        {
            teams = new List<GameObject>[N_OF_TEAMS];
        }

        public void addToTeam(GameObject g, TEAM t)
        {
            teams[t].Add(g);
        }

        public void removeFromTeam(GameObject g, TEAM t)
        {
            teams[t].Remove(g);
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
            //TODO: Do hitbox logic stuff, still need to clarify
            return false;
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
            //TODO: Notify both gameobjects of collision
        }
    }
}