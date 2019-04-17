using BulletHell.character;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace BulletHell.Pickups
{
    public class FastPickup : Pickup
    {
        public FastPickup(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
        }

        public System.Timers.Timer myT = new System.Timers.Timer();
        public bool fast = false;
        private Thread t;
        private Player p;

        private void newTimer()
        {
            myT = new System.Timers.Timer();
            myT.AutoReset = true;
            myT.Interval = 100; //can change for "blink" interval
            myT.Start();
        }

        private void startFast()
        {
            if (fast)
            {
                t.Abort();
            }
            fast = true;
            p.slow = 2;
            t = new Thread(fastRunner);
            t.Start();
        }

        private void fastRunner()
        {
            newTimer();
            Thread.Sleep(4000);
            fast = false;
            p.slow = 0;
            myT.Dispose();

        }


        protected override void onPickup(Player player)
        {
            this.p = player;
            // player.slow = 2;
            startFast();
        }


        
    }
}
