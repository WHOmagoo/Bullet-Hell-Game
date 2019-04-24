using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class Encounter {
        public string enemyType;
        public int timeInMS;
        public Vector2 locationPercentages;

        public Encounter (string type, int startTime, Vector2 startingLocation){
            this.enemyType = type;
            this.timeInMS = startTime;
            this.locationPercentages = startingLocation;
        }
    }
}