using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public interface ILocationEquation
    {
        Vector2 GetLocation(long ticksElapsed);
    }
}