using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public abstract class Bullet
    {
        int damage;
        Path pathToFollow;

        Bullet(int damage, Path path, Rectangle);
    }
}