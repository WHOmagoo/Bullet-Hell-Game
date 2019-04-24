using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class Encounter {
        public string enemyType;
        public int timeInMS;
        public Vector2 locationPercentages;

        public bool isBoss;

        public Encounter (string type, int startTime, Vector2 startingLocation, bool isBoss = false){
            this.enemyType = type;
            this.timeInMS = startTime;
            this.locationPercentages = startingLocation;
            this.isBoss = isBoss;
        }
    }
}