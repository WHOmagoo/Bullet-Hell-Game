using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell
{
    public interface IGameFactory
    {
        Tuple<GameDirector, Canvas> makeGame(GraphicsDevice graphicsDevice);
    }
}