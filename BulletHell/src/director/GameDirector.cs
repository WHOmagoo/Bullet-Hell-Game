using BulletHell.GameEngine;
using PriorityQueue;
using System;

namespace BulletHell.director
{
    public class GameDirector
    {
        private Clock clock;
        private PriorityQueue<DirectorEvent> queue;

        public GameDirector()
        {
            //TODO refactor this to make a new clock when clock is no longer a singleton and then assign Game1 the clock
            this.clock = Clock.getClock();
            queue = new PriorityQueue<DirectorEvent>();
        }

        public void addEvent(long time, DirectorEvent direcetorEvent)
        {
            queue.Add(time, direcetorEvent);
        }

        public void Update()
        {
            foreach (var update in queue.Pop(clock.getTimeSinceLastUpdate()))
            {
                update.Execute();
            }
        }

        public void OnBossDeath(){
            Console.WriteLine("forwarding");
            queue.FastForward();
        }
    }
}