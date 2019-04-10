using System;
using System.Collections.Generic;
using System.Numerics;
using BulletHell;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.ObjectCreation {
    public class EnemyFactory
    {
        public EnemyFactory() {}
        public Enemy makeEnemy(string textureName, int health, Vector2 startingLocation, List<PathData> pathData, string gun)
        {
            Texture2D texture = GraphicsLoader.getGraphicsLoader().getTexture(textureName);
            throw new NotImplementedException();
        }
    }
}