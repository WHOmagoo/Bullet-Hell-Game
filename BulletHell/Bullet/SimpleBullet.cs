namespace BulletHell
{
    public class SimpleBullet : Bullet
    {
        public SimpleBullet(Path path, int locX, int locY, int startingTick) : base(path, locX, locY, startingTick)
        {
        }

        public override double calculateDamage()
        {
            return 1;
        }
    }
}