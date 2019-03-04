using Microsoft.Xna.Framework;

namespace BulletHellTests.GameEngine
{
    public interface ILocationEquation
    {
        Vector2 GetLocation(long ticksElapsed);
    }
}