using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.Pickups {
    public class LifePickup : Pickup
    {
        public LifePickup(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
        }

        protected override void onPickup(Player player)
        {
            player.Lives++;
        }
    }
}
