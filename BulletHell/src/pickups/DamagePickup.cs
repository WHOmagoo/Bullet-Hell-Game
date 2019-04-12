using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BulletHell.Pickups
{
    public class DamagePickup : Pickup
    {
        public DamagePickup(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
        }

        protected override void onPickup(Player player)
        {
            player.gunEquipped = new BasicGun(2, new LinearLocationEquation(-Math.PI / 2, 1),
                           GraphicsLoader.getGraphicsLoader().getBulletTexture(), 500, TEAM.FRIENDLY);
            player.gunEquipped.GunShotHandler += BHGame.Canvas.OnGunShot;
        }

    }
}
