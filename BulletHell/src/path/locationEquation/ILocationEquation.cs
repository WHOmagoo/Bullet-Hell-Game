using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public interface ILocationEquation
    {
        Vector2 GetLocation(long ticksElapsed);
    }
}