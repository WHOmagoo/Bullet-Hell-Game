using BulletHell.GameEngine;
using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public interface Path
    {

        Vector2 InitialLocation {get;}
        Vector2 UpdateLocation();

        /*
            Resets the start time to current time
         */
        void Reset();
    }
}