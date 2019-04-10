using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public interface Path
    {

        Vector2 UpdateLocation();

        /*
            Resets the start time to current time
         */
        void Reset();
    }
}