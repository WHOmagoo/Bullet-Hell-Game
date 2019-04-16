using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.gameEngine;

namespace BulletHell.GameEngine
{
    public class Heart : GameObject
    {
        public Texture2D _heart_texture;
        
        
        public Heart(Texture2D texture,Vector2 startLoc, int width,int height) : base(texture, startLoc, width, height)
        {
            _heart_texture = texture;
        }
        
    }
}
