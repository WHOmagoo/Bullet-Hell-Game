using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BulletHell.Pickups {
    public abstract class Pickup : GameObject{
        public Pickup(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) : base(texture, startLocation, width, height)
        {
        }

        public override void onCollision(GameObject hitby) {
            if(hitby is Player)
            {
                onPickup((Player)hitby);
                BHGame.Canvas.RemoveFromDrawList(this);
                BHGame.CollisionManager.removeFromTeam(this, TEAM.ENEMY);
            }
        }

        protected abstract void onPickup(Player player);

        
    }
}