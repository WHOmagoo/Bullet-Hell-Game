using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BulletHell.Annotations;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class Clock
    {
        private static readonly Clock clock = new Clock();
        private bool isPaused;

        private long speedModifier;

        private BHGame game;

        private long ticksElapsed;
        private long timeSpentPaused;
        private long timeSinceLastUpdate;
        

        public Clock(long speedModifier = 1)
        {
            this.speedModifier = speedModifier;
        }
        
//        public void pause()
//        {
//            if (!isPaused)
//            {
//                Console.WriteLine("Paused game");
//              //  isPaused = true;
//            }
//        }
//
//        public void resume()
//        {
//            Console.WriteLine("Resumed game");
//          //  isPaused = false;
//        }
        
        //Gets time in milliseconds
        public long getTime()
        {
            //10,000,000 ticks per second

            var result = ticksElapsed / TimeSpan.TicksPerMillisecond;
//            (gameTime.TotalGameTime.Ticks - timeSpentPaused.Ticks) / TimeSpan.TicksPerMillisecond;
            // Console.WriteLine("Game Time result {0}", result);
            return result;
        }

        public void setSpeedModifier(long modifier)
        {
            this.speedModifier = modifier;
        }


        public static Clock getClock()
        {
            return clock;
        }

        public void UpdateTime(GameTime time)
        {
            timeSinceLastUpdate = time.ElapsedGameTime.Ticks * speedModifier;
            
            if (isPaused)
            {
                
                timeSpentPaused += timeSinceLastUpdate;
            }
            else
            {
                ticksElapsed += timeSinceLastUpdate;
            }
        }

        public long getTimeSinceLastUpdate()
        {
            return 10 * timeSinceLastUpdate / TimeSpan.TicksPerMillisecond;
        }

//        public void OnPause(object sender, EventArgs e)
//        {
//            if (isPaused)
//            {
//                resume();
//            }
//            else
//            {
//                pause();
//            }
//        }
    }
}