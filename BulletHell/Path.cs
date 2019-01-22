using System;

namespace BulletHell
{
    public abstract class Path
    {
        public abstract Tuple<double, double> updateLocation(Tuple<double, double> curLocation, int currentTick);
    }
}