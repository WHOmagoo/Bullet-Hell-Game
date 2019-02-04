using System;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public abstract class Bullet
    {
        private int lastUpdate;
        private Vector2 currentLocation;

        private Path _path;

        public Bullet(Path path, int locX, int locY)
        {
            this._path = path;
            
            //Save the current location of the bullet
            currentLocation = new Vector2(locX, locY);
        }

        public abstract double calculateDamage();

        public void updateLocation()
        {
            currentLocation = _path.UpdateLocation();
            //TODO implement the notify pattern to alert subscribed objects that our location has changed
        }
    }
}