using System;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public abstract class Bullet : GameObject
    {
        int damage;
        Path pathToFollow;

        //Bullet(int damage, Path path, Rectangle);
    }
}