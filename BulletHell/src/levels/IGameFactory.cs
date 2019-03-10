using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.levels
{
    public interface IGameFactory
    {
        // Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice);
        BHGame makeGame();
    }
}