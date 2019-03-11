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
        private GameTime gameTime;
        private bool isPaused;
        private TimeSpan timeLastPausedOccured;
        private TimeSpan timeSpentPaused = TimeSpan.Zero;

        private long speedModifier;
        
        private BHGame game;

        private long ticksElapsed;
        private long timeSinceLastUpdate;
        

        public Clock(long speedModifier = 1)
        {
            this.speedModifier = speedModifier;
        }
        
        public void pause()
        {
            if (!isPaused)
            {
                isPaused = true;
                timeLastPausedOccured = gameTime.TotalGameTime;
            }
        }

        public void resume()
        {
            isPaused = false;
            timeSpentPaused = new TimeSpan(timeSpentPaused.Ticks + timeLastPausedOccured.Ticks);
            timeLastPausedOccured = TimeSpan.Zero;
        }
        
        //Gets time in milliseconds
        public long getTime()
        {
            if (ReferenceEquals(gameTime, null))
            {
                return 0;
            }
            
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
            gameTime = time;
            timeSinceLastUpdate = time.ElapsedGameTime.Ticks * speedModifier;
            ticksElapsed += time.ElapsedGameTime.Ticks * speedModifier;
        }

        public long getTimeSinceLastUpdate()
        {
            return 10 * timeSinceLastUpdate / TimeSpan.TicksPerMillisecond;
        }
    }
}