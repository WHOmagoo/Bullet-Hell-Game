using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class Encounter {
        public string enemyType;
        public int timeInMS;
        public Vector2 locationPercentages;
        public string weaponType;

        public Encounter (string type, string weaponType, int startTime, Vector2 startingLocation){
            this.enemyType = type;
            this.timeInMS = startTime;
            this.locationPercentages = startingLocation;
            this.weaponType = weaponType;
        }
    }
}