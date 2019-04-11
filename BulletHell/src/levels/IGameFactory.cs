using System;
using BulletHell.controls;
using BulletHell.director;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.levels
{
    public interface IGameFactory
    {
        Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller);
    }
}