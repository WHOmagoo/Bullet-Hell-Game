namespace BulletHell.ObjectCreation {
    public struct PathData {
        public string equationType;
        public long pathDuration; //Milliseconds
        public double angleOffset; //Radians
        public double speed; //(ratio) 1 being normal

        public PathData(string equationType, long pathDuration, double angleOffset, double speed)
        {
            this.equationType = equationType;
            this.pathDuration = pathDuration;
            this.angleOffset = angleOffset;
            this.speed = speed;
        }
    }
}