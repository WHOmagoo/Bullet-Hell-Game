using System;
using Microsoft.Xna.Framework;

namespace BulletHellTests
{
    public class Clock
    {
        private static readonly Clock clock = new Clock();
        private GameTime gameTime;
        private bool isPaused;
        private TimeSpan timeLastPausedOccured;
        private TimeSpan timeSpentPaused = TimeSpan.Zero;

        private long speedModifier;
        
        private Game1 game;

        private long ticksElapsed;
        private long timeSinceLastUpdate;
        

        private Clock()
        {
            speedModifier = 1;
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
        
        public long getTime()
        {
            if (ReferenceEquals(gameTime, null))
            {
                return 0;
            }
            
            //10,000,000 ticks per second
            
            var result =  (gameTime.TotalGameTime.Ticks - timeSpentPaused.Ticks) / TimeSpan.TicksPerMillisecond;
            // Console.WriteLine("Game Time result {0}", result);
            return result;
        }


        public static Clock getClock()
        {
            return clock;
        }

        public void Update()
        {
            timeSinceLastUpdate = gameTime.ElapsedGameTime.Ticks;
            ticksElapsed += gameTime.ElapsedGameTime.Ticks;
        }

        public long getTimeSinceLastUpdate()
        {
            return timeSinceLastUpdate;
        }

        public void SetGameTime(GameTime gameTime)
        {
            if (!ReferenceEquals(this.gameTime, gameTime))
            {
                this.gameTime = gameTime;
            }
        }
    }
}