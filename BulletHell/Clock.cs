using System.Threading;

namespace BulletHell
{
    public class Clock
    {
        private static readonly Clock clock = new Clock();
        private volatile int time = 0;
        private Thread t;

        private Clock()
        {
            t = new Thread(clockLoop);   
        }

        public void startClock()
        {
            t.Start();
        }

        private void clockLoop()
        {
            while (true)
            {
                //50 updates per second
                Thread.Sleep(1000 / 50);

                //each update will increase the time by 24 ticks
                //That means we have 1200 ticks per second 
                time += 24;
            }
            
        }
        
        public int getTime()
        {
            return time;
        }


        public static Clock getClock()
        {
            return clock;
        }
    }
}