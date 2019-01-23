using System;

namespace BulletHell
{
    public abstract class Bullet
    {
        private int lastUpdate;
        private Tuple<double, double> currentLocation;
        private Tuple<double, double> offset;
        private int startingTick;

        private Path path;

        public Bullet(Path path, int locX, int locY, int startingTick)
        {
            this.path = path;
            
            //Save the global starting tick that this object was created on
            this.startingTick = startingTick;
            
            //Save the current location of the bullet
            currentLocation = new Tuple<double, double>(locX, locY);
            
            //tmp variable of the initial position based upon the path
            Tuple<double, double> tmp = path.updateLocation(0);
            
            //Calculate the x and y translations of the bullet when compared to thee path
            offset = new Tuple<double, double>(tmp.Item1 - locX, tmp.Item2 - locY);
        }

        public abstract double calculateDamage();

        public void updateLocation(int newTime)
        {
            //Find the new location the bullet should be in. We pass in the amount of ticks elapsed since the object started moving
            Tuple<double, double> tmp = path.updateLocation(newTime - startingTick);
            
            //Account for the initial calculated translation in final output of update location
            currentLocation = new Tuple<double, double>(tmp.Item1 + offset.Item1, tmp.Item2 + offset.Item2);
            
            //TODO implement the notify pattern to alert subscribed objects that our location has changed
        }
    }
}