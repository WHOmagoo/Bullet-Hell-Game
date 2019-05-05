using BulletHell.bullet.factory;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
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
            // player.gunEquipped = new BasicGun(2, new LinearLocationEquation(-Math.PI / 2, 1),
            //                GraphicsLoader.getGraphicsLoader().getTexture("bullet"), 500, TEAM.FRIENDLY);
            // player.gunEquipped.GunShotHandler += BHGame.Canvas.OnGunShot;
            //FIXME:
            player.gunEquipped = new Gun(1, GraphicsLoader.getGraphicsLoader().getBulletTexture(),
                BulletFactoryFactory.make("surround"), TEAM.FRIENDLY);
            player.gunEquipped.GunShotHandler += BHGame.Canvas.OnGunShot;
        }

    }
}
