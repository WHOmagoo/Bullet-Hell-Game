using System;
using System.Collections.Generic;

namespace BulletHell.gameEngine
{
    // const int N_OF_TEAMS = 3;
    public enum TEAM { FRIENDLY = 0, ENEMY, UNASSIGNED }
    public class CollisionManager
    {
        List<GameObject>[] teams;
        List<Tuple<GameObject, TEAM>> addBuffer; //For storing gameobjects to add after collisions complete.
        List<Tuple<GameObject, TEAM>> removeBuffer; //For storing gameobjects to remove after collisions complete.
        Boolean runningCollisions;
        const int N_OF_TEAMS = 3; //= Enum.GetNames(TEAM).Length; //FIXME: make const and dynamically get nteams

        public CollisionManager()
        {
            teams = new List<GameObject>[N_OF_TEAMS];
            for (int i = 0; i < N_OF_TEAMS; i++)
            {
                teams[i] = new List<GameObject>();
            }
            addBuffer = new List<Tuple<GameObject, TEAM>>();
            removeBuffer = new List<Tuple<GameObject, TEAM>>();
            runningCollisions = false;
        }

        public void addToTeam(GameObject g, TEAM t)
        {
            if (g.Hitbox == null)
                throw new NullReferenceException("Gameobject doesn't have a hitbox for collision manager");
            else if (runningCollisions)
                addBuffer.Add(new Tuple<GameObject, TEAM>(g, t));
            else
                teams[(int)t].Add(g);
        }

        public void removeFromTeam(GameObject g, TEAM t)
        {
            if (runningCollisions)
                removeBuffer.Add(new Tuple<GameObject, TEAM>(g, t));
            else
                teams[(int)t].Remove(g);
        }

        public void flushBuffers()
        {
            if (removeBuffer.Count > 0 || addBuffer.Count > 0)
            {

                foreach (Tuple<GameObject, TEAM> gt in removeBuffer)
                {
                    GameObject g = gt.Item1;
                    TEAM t = gt.Item2;
                    removeFromTeam(g, t);
                }
                removeBuffer.Clear();
                foreach (Tuple<GameObject, TEAM> gt in addBuffer)
                {
                    GameObject g = gt.Item1;
                    TEAM t = gt.Item2;
                    addToTeam(g, t);
                }
                addBuffer.Clear();
            }
        }
        /**
            Goes through different teams and runs collision checks. If collided
            notifies the corresponding GameObjects
         */
        public void runCollisions()
        {
            runningCollisions = true;
            for (int i = 0; i < N_OF_TEAMS; i++)
            {
                for (int j = i + 1; j < N_OF_TEAMS; j++)
                {
                    runCollisionsBetweenTeams(teams[i], teams[j]);
                }
            }
            runningCollisions = false;
            flushBuffers();
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
                    if (isColliding(g1, g2))
                        onCollide(g1, g2);
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