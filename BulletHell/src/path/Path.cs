using BulletHell.GameEngine;
using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public abstract class Path
    {

        protected long StartTime;
        protected Vector2 _initialLocation;
        public Vector2 InitialLocation {get{return _initialLocation;}}
        public abstract Vector2 UpdateLocation();

        /*
            Resets the start time to current time
         */
        public void Reset()
        {
            this.StartTime = Clock.getClock().getTime();
        }
        public void ResetAt(Vector2 newStartingLocation)
        {
            _initialLocation = newStartingLocation;
        }
    }
}