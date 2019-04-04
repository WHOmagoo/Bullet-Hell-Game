using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Pickups {
    public class Pickup : GameObject{
        public Pickup(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
        }
    }
}