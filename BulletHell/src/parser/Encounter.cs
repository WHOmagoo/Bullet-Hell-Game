using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell
{
    public class Encounter {
        string enemyType;
        int timeInMS;
        Vector2 locationPercentages;

        public Encounter (string type, int startTime, Vector2 startingLocation){
            this.enemyType = type;
            this.timeInMS = startTime;
            this.locationPercentages = startingLocation;
        }
    }
}