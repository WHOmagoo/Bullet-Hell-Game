using System;
using System.Collections.Generic;

namespace BulletHell
{
    public class PathData{
        string pathType;
        int pathDuration;
        double offsetRadians;
        int speed;

        public PathData (string pathName, int duration, double offset, int speed){
            this.pathType = pathName;
            this.pathDuration = duration;
            this.offsetRadians = offset;
            this.speed = speed;
        }
    }
}